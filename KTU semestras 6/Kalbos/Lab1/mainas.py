import Cube
import Cubes


pCubes = list()
us = Cubes.Cubes(24)
us.populate()
#print(us.getCount())
for index in range(us.getCount()):
    if us.isPerfectCube(index) is not None:
        pCubes.append(us.isPerfectCube(index))

us.printAll()

for cube in pCubes:
    cube.print()
    #print("a3 = " + str( pow(cube.a, 3) ) + " b3 + c3 + d3 = " + str(pow(cube.b, 3) + pow(cube.c, 3) + pow(cube.d, 3)) )
