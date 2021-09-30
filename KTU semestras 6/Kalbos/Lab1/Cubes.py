import Cube

class Cubes:
    def __init__(self, aCount):
        self.aCount = aCount
        self.cubeList = list()

    def populate(self):
        count = 0
        for a in range(1, self.aCount + 1):
            if a == 1:
                for b in range(1, 21):
                    for c in range(1, 21):
                        for d in range(1, 21):
                            if not self.cubeList.__contains__(Cube.Cube(a, b, c, d)):
                                self.cubeList.append(Cube.Cube(a, b, c, d))
                                count = len(self.cubeList)
            else:
                for x in range(0, count):
                    self.cubeList.append(Cube.Cube(a, self.cubeList[x].b, self.cubeList[x].c, self.cubeList[x].d))


    def printAll(self):
        for x in self.cubeList:
            x.print()

    def isPerfectCube(self, index):
        if self.cubeList[index].isPerfectCube():
            return self.cubeList[index]
        else:
            return None

    def getCount(self):
        return len(self.cubeList)

    def appendCube(self, cube):
        self.cubeList.append(cube)
