#include <iostream>
#include <string>
#include "omp.h"
#include <fstream>
#include "rapidjson\document.h"
#include "rapidjson/writer.h"
#include "rapidjson/stringbuffer.h"
#include "rapidjson\istreamwrapper.h"
#include <windows.h>
#include <iomanip>

using namespace std;
using namespace rapidjson;

#pragma region Automobilis

class Automobilis
{
private:
	string marke;
	double rida;
	int metai;
	double sandauga;

public:
	Automobilis(string, int, double);
	Automobilis();
	~Automobilis();

	string getMarke() {
		return marke;
	}

	int getMetai() {
		return metai;
	}

	double getRida() {
		return rida;
	}

	double getSandauga() {
		return sandauga;
	}

	void insertSandauga(double sandauga)
	{
		this->sandauga = sandauga;
	}

	int CompareTo(Automobilis automobilis) {
		if (getMarke().compare(automobilis.getMarke()) > 0) {
			return 1;
		}
		else if (getMarke().compare(automobilis.getMarke()) < 0) {
			return -1;
		}
		else
		{
			if (getMetai() > automobilis.getMetai())
			{
				return 1;
			}
			else if (getMetai() < automobilis.getMetai())
			{
				return -1;
			}
			else
			{
				if (getRida() > automobilis.getRida())
				{
					return 1;
				}
				else if (getRida() < automobilis.getRida())
				{
					return -1;
				}
				else
				{
					return 0;
				}
			}
		}
	}
};

Automobilis::Automobilis(string marke, int metai, double rida) {
	this->marke = marke;
	this->metai = metai;
	this->rida = rida;
	this->sandauga = 0;
}

Automobilis::Automobilis() {
	this->marke = "";
	this->metai = 0;
	this->rida = 0;
	this->sandauga = 0;
}
Automobilis::~Automobilis() {
	
}

#pragma endregion

#pragma region Automobiliai

class Automobiliai {
private:
	Automobilis *automobiliai;
	int memberCount;
	int arraySize;
	omp_lock_t lock;
	bool busAuto;

public:
	Automobiliai(int);
	Automobiliai();
	~Automobiliai();


	void insert(Automobilis automobilis) {

		
		omp_set_lock(&lock);
		while (this->memberCount == this->arraySize) {
			omp_unset_lock(&lock);
			Sleep(0.1);
			omp_set_lock(&lock);
		}
		/*string eilute = automobilis.getMarke() + " " + to_string(automobilis.getMetai()) + " " + to_string(automobilis.getRida()) + "\n";
		printf(eilute.c_str());*/
		for (int i = 0; i < arraySize; i++) {
			if (automobiliai[i].getMarke() == "" && automobiliai[i].getMetai() == 0 && automobiliai[i].getRida() == 0 && automobiliai[i].getSandauga() == 0) {
				automobiliai[i] = automobilis;
				break;
			}
			else if (automobiliai[i].CompareTo(automobilis) == -1) {
				continue;
			}
			else {
				for (int j = arraySize - 1; j > i; j--) {
					automobiliai[j] = automobiliai[j - 1];
				}
				automobiliai[i] = automobilis;
				break;
			}
		}
		memberCount++;
		omp_unset_lock(&lock);
	}

	Automobilis getAuto()
	{
		Automobilis automobilis;
		
		omp_set_lock(&lock);
		
		while (this->memberCount == 0)
		{
			omp_unset_lock(&lock);
			if (busAuto == false) {
				return Automobilis();
			}
			Sleep(0.1);
			omp_set_lock(&lock);			
		}

		automobilis = automobiliai[0];

		/*string eilute = automobilis.getMarke() + " " + to_string(automobilis.getMetai()) + " " + to_string(automobilis.getRida()) + "\n";
		printf(eilute.c_str());*/
		
		remove();
		omp_unset_lock(&lock);
		return automobilis;
	}

	void remove() {		
		for (int i = 0; i < arraySize - 1; i++)
		{
			if (i == arraySize - 1)
			{
				automobiliai[i] = Automobilis();
				break;
			}
			automobiliai[i] = automobiliai[i + 1];
		}
		memberCount--;
	}

	int getArraySize()
	{
		return arraySize;
	}

	int getMemberCount()
	{
		return memberCount;
	}

	void setBusAuto(bool busAuto) {
		this->busAuto = busAuto;
	}
};

Automobiliai::Automobiliai(int size) {
	this->automobiliai = new Automobilis[size];
	this->arraySize = size;
	this->memberCount = 0;
	omp_init_lock(&lock);
	this->busAuto = true;
}
Automobiliai::Automobiliai() {
	this->automobiliai = new Automobilis[0];
	this->arraySize = 0;
	this->memberCount = 0;
	omp_init_lock(&lock);
	this->busAuto = true;
}
Automobiliai::~Automobiliai() {

}


#pragma endregion

Automobilis Skaiciavimas(Automobilis);

int main()
{
	Automobilis autoArr[28];

	Automobiliai automobiliai((*(&autoArr + 1) - autoArr) / 2);

	Automobiliai rezultatai(*(&autoArr + 1) - autoArr);

	#pragma region skaitymas

	ifstream ifs{ R"(C:\Users\vyten\Desktop\Lab1a LP\IFF-72_KunickasV_L1_dat_3.json)" };
	if (!ifs.is_open())
	{
		std::cerr << "Could not open file for reading!\n";
		return EXIT_FAILURE;
	}
	IStreamWrapper isw{ ifs };
	Document doc{};
	doc.ParseStream(isw);
	StringBuffer buffer{};
	Writer<StringBuffer> writer{ buffer };
	doc.Accept(writer);
	if (doc.HasParseError())
	{
		std::cout << "Error  : " << doc.GetParseError() << '\n'
			<< "Offset : " << doc.GetErrorOffset() << '\n';
		return EXIT_FAILURE;
	}
	int i = 0;
	for (const auto& d : doc.GetArray()) {
		autoArr[i] = Automobilis(d["marke"].GetString(), d["metai"].GetInt(), d["rida"].GetDouble());;
		i++;
	}

	#pragma endregion skaitymas

	omp_set_num_threads(8);

	#pragma omp parallel
	{
		if (omp_get_thread_num() == 0) {
			for (Automobilis var : autoArr)
			{
				automobiliai.insert(var);
			}
		}
		else {
			for (int j = 0; j < 4; j++) {
				Automobilis startAuto = automobiliai.getAuto();
				Automobilis rezAuto = Skaiciavimas(startAuto);
				if(rezAuto.getSandauga() > 500000000)
					rezultatai.insert(rezAuto);
			}
		}	
	}

	rezultatai.setBusAuto(false);

	ofstream out; 
	out.open(R"(C:\Users\vyten\Desktop\Lab1b LP\IFF-72_KunickasV_L1_rez.txt)", ios_base::app);
	out << "IFF-7/2_KunickasV_L1_dat_3.json\n";
	out << "Pradiniai duomenys\n";
	out << setw(13) << "Marke|" << setw(13) << "Metai|" << setw(20) << "Rida|" << setw(20) << "\n";
	out << "------------------------------------------------------------------\n";
	for (int j = 0; j < 28; j++) {
		out << setw(13) << autoArr[j].getMarke() << setw(13) << autoArr[j].getMetai() << setw(21) << autoArr[j].getRida() << setprecision(13) << "\n";
	}

	out << "\nRezultatai\n";
	out << setw(13) << "Marke|" << setw(13) << "Metai|" << setw(20) << "Rida|" << setw(20) << "Sandauga"  << "\n";
	out << "------------------------------------------------------------------\n";
	int kiekis = rezultatai.getMemberCount();
	for (int j = 0; j < kiekis; j++) {
		Automobilis rezAuto = rezultatai.getAuto();
		out << setw(13) << rezAuto.getMarke() << setw(13) << rezAuto.getMetai() << setw(21) << rezAuto.getRida() << setprecision(13) << setw(21) << rezAuto.getSandauga() << setprecision(13) << "\n";
	}
	out << "\n";
	out.close();

	return 0;
}

Automobilis Skaiciavimas(Automobilis automobilis) {
	Automobilis retAutomobilis = automobilis;

	double sandauga = retAutomobilis.getMarke().size() * retAutomobilis.getRida() * retAutomobilis.getMetai();

	retAutomobilis.insertSandauga(sandauga);

	return retAutomobilis;
}