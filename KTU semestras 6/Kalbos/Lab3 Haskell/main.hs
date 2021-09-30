--import Data.list
import System.IO

pradDuomenys :: [[Double]]
pradDuomenys = [[20, 80, -40, -20, 120, -20],
        [20, -20, 120, -20, -40, 80],
        [0, 0, 1, 0, 0, 1],
        [0, 0, 999, 1, -999, 1],
        [897, -916, 847, -972, 890, -925],
        [999, 999, -999, -998, -998, -999],
        [-999, -999, 999, -999, 0, 731],
        [-999, -999, 999, -464, -464, 999],
        [979, -436, -955, -337, 157, -439]]

lineLength :: Double -> Double -> Double -> Double -> Double
lineLength x y x1 y1 = sqrt ((x - x1)^2 + (y - y1)^2)

semiPerimeter :: Double -> Double -> Double -> Double -> Double -> Double -> Double
semiPerimeter x y x1 y1 x2 y2 = ((lineLength x y x1 y1) + (lineLength x1 y1 x2 y2) + (lineLength x y x2 y2))/2

triangleArea :: Double -> Double -> Double -> Double -> Double
triangleArea s a b c = sqrt ( s * ( s - a ) * ( s - b ) * ( s -c ) )

inradius :: Double -> Double -> Double -> Double -> Double -> Double -> Double
inradius x y x1 y1 x2 y2 = (triangleArea (semiPerimeter x y x1 y1 x2 y2) (lineLength x y x1 y1) (lineLength x1 y1 x2 y2) (lineLength x y  x2 y2)) / (semiPerimeter x y x1 y1 x2 y2)

centreXY :: Double -> Double -> Double -> Double -> Double -> Double -> [Double]
centreXY x y x1 y1 x2 y2 = [x0, y0] where 
        x0 = (a * x2 + b * x1 + c * x) / (a + b + c)
        y0 = (a * y2 + b * y1 + c * y) / (a + b + c)
        a = lineLength x y x1 y1
        b = lineLength x y x2 y2
        c = lineLength x1 y1 x2 y2

{-
a = lineLength 20 80 (-40) (-20)
b = lineLength 20 80 120 (-20)
c = lineLength (-40) (-20) 120 (-20)
x0 = (a * 120 + b * (-40) + c * 20) / (a + b + c)
y0 = (a * (-20) + b * (-20) + c * 80) / (a + b + c)
radius = inradius 20 80 (-40) (-20) 120 (-20)

d = lineLength x0 y0 20 80
e = lineLength x0 y0 (-40) (-20)
f = lineLength x0 y0 120 (-20)

centre = centreXY 20 80 (-40) (-20) 120 (-20)
-}

malfatti :: Double -> Double -> Double -> Double -> Double -> Double -> [Double]
malfatti x y x1 y1 x2 y2 = [r1, r2, r3] where
        r1 = (r * (s - r + d - e - f)) / (2 * (s - c))
        r2 = (r * (s - r - d + e - f)) / (2 * (s - b))
        r3 = (r * (s - r - d - e + f)) / (2 * (s - a))
        r = inradius x y x1 y1 x2 y2
        s = semiPerimeter x y x1 y1 x2 y2
        d = lineLength x0 y0 x y
        e = lineLength x0 y0 x1 y1
        f = lineLength x0 y0 x2 y2
        x0 = (centreXY x y x1 y1 x2 y2)!!0
        y0 = (centreXY x y x1 y1 x2 y2)!!1
        a = lineLength x y x1 y1
        b = lineLength x y x2 y2
        c = lineLength x1 y1 x2 y2

{-
smt = malfatti 20 80 (-40) (-20) 120 (-20)
-}

malfattiFromList :: [Double] -> [Double]
malfattiFromList duomenys = atsakymas where
        atsakymas = malfatti (duomenys!!0) (duomenys!!1) (duomenys!!2) (duomenys!!3) (duomenys!!4) (duomenys!!5)

result = map malfattiFromList pradDuomenys