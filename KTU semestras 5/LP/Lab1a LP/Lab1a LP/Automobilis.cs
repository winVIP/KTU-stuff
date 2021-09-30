using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1a_LP
{
    class Automobilis : IComparable
    {
        string marke;
        double rida;
        int metai;
        double sandauga;
        
        public Automobilis(string marke, int metai, double rida)
        {
            this.marke = marke;
            this.rida = rida;
            this.metai = metai;
        }

        public string getMarke()
        {
            return marke;
        }

        public int getMetai()
        {
            return metai;
        }
        public double getRida()
        {
            return rida;
        }

        public void insertSandauga(double sandauga)
        {
            this.sandauga = sandauga;
        }

        public double getSandauga()
        {
            return sandauga;
        }

        public int CompareTo(object obj)
        {
            Automobilis kitasAuto = (Automobilis)obj;
            if(String.Compare(getMarke(), kitasAuto.getMarke()) == 1)
            {
                return 1;
            }
            else if (String.Compare(getMarke(), kitasAuto.getMarke()) == -1)
            {
                return -1;
            }
            else
            {
                if(getMetai() > kitasAuto.getMetai())
                {
                    return 1;
                }
                else if(getMetai() < kitasAuto.getMetai())
                {
                    return -1;
                }
                else
                {
                    if(getRida() > kitasAuto.getRida())
                    {
                        return 1;
                    }
                    else if (getRida() < kitasAuto.getRida())
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
    }
}
