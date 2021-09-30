import math

def Trapezoid (x, a, b, c, d):
    if(x < a):
        return 0
    elif(a <= x and x < b):
        return (x - a) / (b - a)
    elif(b <= x and x < c):
        return 1
    elif(c <= x and x < d):
        return (d - x) / (d - c)
    elif(d <= x):
        return 0

def Triangle (x, a, b, c):
    if(x < a):
        return 0
    elif(a <= x and x < b):
        return (x - a) / (b - a)
    elif(b <= x and x <= c):
        return (c - x) / (c - b)
    elif(x > c):
        return 0

def TriangleArea (ax, ay, bx, by, cx, cy):
    return abs((ax*(by-cy) + bx*(cy-ay) + cx(ay-by)) / 2)

def lineLength(ax, ay, bx, by):
    x = pow(ax-bx, 2)
    y = pow(ay-by, 2)
    return math.sqrt(x+y)

def SquareArea (ax, ay, bx, by, cx, cy, dx, dy):
    a = lineLength(ax, ay, bx, by)
    b = lineLength(bx, by, cx, cy)
    return a * b

def Kaina(x):
    mazaKaina = Trapezoid(x, 0, 0, 30000, 40000)
    vidutineKaina = Triangle(x, 30000, 60000, 90000)
    dideleKaina = Trapezoid(x, 80000, 110000, 130000, 130000)
    return [mazaKaina, vidutineKaina, dideleKaina]

def Amzius(x):
    nauja = Trapezoid(x, 0, 0, 5, 8)
    vidutinioAmziaus = Triangle(x, 6, 10, 15)
    sena = Trapezoid(x, 13, 18, 100, 100)
    return [nauja, vidutinioAmziaus, sena]

def Galingumas(x):
    negalinga = Trapezoid(x, 0, 0, 60, 130)
    sportine = Triangle(x, 100, 300, 500)
    labaiGalinga = Trapezoid(x, 400, 700, 1000, 1000)
    return [negalinga, sportine, labaiGalinga]


def Taisykle1(kaina, amzius, galingumas):
    return max(Kaina(kaina)[2], Amzius(amzius)[2], Galingumas(galingumas)[0])

def Taisykle2(kaina, amzius, galingumas):
    return max(Kaina(kaina)[1], Amzius(amzius)[1], Galingumas(galingumas)[1])

def Taisykle3(kaina, amzius, galingumas):
    return max(Kaina(kaina)[0], Amzius(amzius)[0], Galingumas(galingumas)[2])

def Taisykle4(kaina, amzius, galingumas):
    return min(Kaina(kaina)[2], Amzius(amzius)[2], Galingumas(galingumas)[1])

def Taisykle5(kaina, amzius, galingumas):
    return min(Kaina(kaina)[2], Amzius(amzius)[2], Galingumas(galingumas)[2])

def Taisykle6(kaina, amzius, galingumas):
    return min(Kaina(kaina)[1], Amzius(amzius)[0], Galingumas(galingumas)[1])

def Taisykle7(kaina, amzius, galingumas):
    return min(Kaina(kaina)[1], Amzius(amzius)[1], Galingumas(galingumas)[1])

def Taisykle8(kaina, amzius, galingumas):
    return min(Kaina(kaina)[0], Amzius(amzius)[1], Galingumas(galingumas)[2])

def Taisykle9(kaina, amzius, galingumas):
    return min(Kaina(kaina)[0], Amzius(amzius)[0], Galingumas(galingumas)[1])

def lineEquation(ax, ay, bx, by):
    m = (by - ay) / (bx - ax)
    b = ay - m * ax
    return [m, b]

def MOM(kaina, amzius, galingumas):
    t1 = Taisykle1(kaina, amzius, galingumas)
    t2 = Taisykle2(kaina, amzius, galingumas)
    t3 = Taisykle3(kaina, amzius, galingumas)
    t4 = Taisykle4(kaina, amzius, galingumas)
    t5 = Taisykle5(kaina, amzius, galingumas)
    t6 = Taisykle6(kaina, amzius, galingumas)
    t7 = Taisykle7(kaina, amzius, galingumas)
    t8 = Taisykle8(kaina, amzius, galingumas)
    t9 = Taisykle9(kaina, amzius, galingumas)
    maximum = max(t1, t2, t3, t4, t5, t6, t7, t8, t9)
    result = 0
    if(t1 == maximum or t4 == maximum or t5 == maximum):
        biggestT = max(t1, t4, t5)
        a = 0
        b = 0
        c = 20
        d = 30
        mb = lineEquation(c, 1, d, 0)
        x = (biggestT - mb[1]) / mb[0]
        result = (x + a) / 2
    if(t2 == maximum or t6 == maximum or t7 == maximum):
        biggestT = max(t2, t6, t7)
        a = 20
        b = 40
        c = 60
        mb1 = lineEquation(a, 0, b, 1)
        mb2 = lineEquation(b, 1, c, 0)
        x1 = (biggestT - mb1[1]) / mb1[0]
        x2 = (biggestT - mb2[1]) / mb2[0]
        result = (x2 + x1) / 2
    if(t3 == maximum or t8 == maximum or t9 == maximum):
        biggestT = max(t3, t8, t9)
        a = 50
        b = 70
        c = 100
        d = 100
        mb = lineEquation(a, 0, b, 1)
        x = (biggestT - mb[1]) / mb[0]
        result = (c + x) / 2
    return result
    
def SOM(kaina, amzius, galingumas):
    t1 = Taisykle1(kaina, amzius, galingumas)
    t2 = Taisykle2(kaina, amzius, galingumas)
    t3 = Taisykle3(kaina, amzius, galingumas)
    t4 = Taisykle4(kaina, amzius, galingumas)
    t5 = Taisykle5(kaina, amzius, galingumas)
    t6 = Taisykle6(kaina, amzius, galingumas)
    t7 = Taisykle7(kaina, amzius, galingumas)
    t8 = Taisykle8(kaina, amzius, galingumas)
    t9 = Taisykle9(kaina, amzius, galingumas)
    maximum = max(t1, t2, t3, t4, t5, t6, t7, t8, t9)
    result = 0
    if(t1 == maximum or t4 == maximum or t5 == maximum):
        a = 0
        b = 0
        c = 20
        d = 30
        if(max(t1, t4, t5) < 1):
            biggestT = max(t1, t4, t5)
            mb = lineEquation(c, 1, d, 0)
            x = (biggestT - mb[1]) / mb[0]
            result = x
        else:
            result = a
    elif(t2 == maximum or t6 == maximum or t7 == maximum):
        a = 20
        b = 40
        c = 60
        if(max(t2, t6, t7) < 1):
            biggestT = max(t2, t6, t7)
            mb1 = lineEquation(a, 0, b, 1)
            x1 = (biggestT - mb1[1]) / mb1[0]
            result = x1
        else:
            result = b
    elif(t3 == maximum or t8 == maximum or t9 == maximum):
        a = 50
        b = 70
        c = 100
        d = 100
        if(max(t3, t8, t9) < 1):
            biggestT = max(t3, t8, t9)
            mb = lineEquation(a, 0, b, 1)
            x = (biggestT - mb[1]) / mb[0]
            result = x
        else:
            result = b
    return result

def Agregacija(kaina, amzius, galingumas):
    t1 = Taisykle1(kaina, amzius, galingumas)
    t2 = Taisykle2(kaina, amzius, galingumas)
    t3 = Taisykle3(kaina, amzius, galingumas)
    t4 = Taisykle4(kaina, amzius, galingumas)
    t5 = Taisykle5(kaina, amzius, galingumas)
    t6 = Taisykle6(kaina, amzius, galingumas)
    t7 = Taisykle7(kaina, amzius, galingumas)
    t8 = Taisykle8(kaina, amzius, galingumas)
    t9 = Taisykle9(kaina, amzius, galingumas)
    maximum = max(t1, t2, t3, t4, t5, t6, t7, t8, t9)
    result = -1
    if(t1 == maximum or t4 == maximum or t5 == maximum):
        result = 0
    if(t2 == maximum or t6 == maximum or t7 == maximum):
        result = 1
    if(t3 == maximum or t8 == maximum or t9 == maximum):
        result = 2
    return ["Maza", "Vidutine", "Didele"].__getitem__(result)

pdKaina = 60000
pdAmzius = 10
pdGalingumas = 300
print("Kaina: " + pdKaina.__str__() + " Amzius: " + pdAmzius.__str__() + " Galingumas: " + pdGalingumas.__str__())
print("Agregacijos rezultatas: " + Agregacija(pdKaina, pdAmzius, pdGalingumas))
print("MOM: " + MOM(pdKaina, pdAmzius, pdGalingumas).__str__())
print("SOM: " + SOM(pdKaina, pdAmzius, pdGalingumas).__str__())

print(Trapezoid(9.5, 6, 13, 20, 20))

print(Triangle(9.5, 2, 8, 14))