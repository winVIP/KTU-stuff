import math

class Funkcijos:
    def Vidurkis(self, data):
        suma = 0
        for x in data:
            suma = suma + x
        vidurkis = suma / len(data)
        return vidurkis

    def Mediana(self, data):
        data.sort()
        result = None
        if len(data) % 2 == 0:
            result = (data[int(len(data) / 2)] + data[int(len(data) / 2 + 1)]) / 2
        else:
            result = data[math.ceil((len(data) / 2))]
        return result
            
    def Minimum(self, data):
        minimum = data[0]
        for x in data:
            if x < minimum:
                minimum = x
        return minimum

    def Maximum(self, data):
        maximum = data[0]
        for x in data:
            if x > maximum:
                maximum = x
        return maximum

    def Quartiles(self, data):
        data.sort()
        rList = list()
        firstHalf = list()
        secondHalf = list()
        if len(data) % 2 == 0:
            firstHalf = data[:int(len(data) / 2)]
            secondHalf = data[int(len(data) / 2):]
        else:
            firstHalf = data[:math.ceil(len(data) / 2) - 1]
            secondHalf = data[math.ceil(len(data) / 2):]
        rList.append(Funkcijos.Mediana(None, firstHalf))
        rList.append(Funkcijos.Mediana(None, secondHalf))
        return rList

    def Moda(self, data):
        mFrequency = 0
        result = data[0]

        for x in data:
            cFrequency = data.count(x)
            if mFrequency < cFrequency:
                mFrequency = cFrequency
                result = x

        return result

    def Moda2nd(self, data):
        moda1st = Funkcijos.Moda(None, data)

        mFrequency = 0
        result = data[0]

        for x in data:
            if x == moda1st: continue
            else:
                cFrequency = data.count(x)
                if mFrequency < cFrequency:
                    mFrequency = cFrequency
                    result = x

        return result

    def StandartDeviation(self, data):
        avarage = Funkcijos.Vidurkis(None, data)
        sum = 0
        for x in data:
            sum = sum + math.pow(x - avarage, 2)
        mean = sum / len(data)
        return math.sqrt(mean)

            