class Cube:
    def __init__(self, a, b, c, d):
        self.a = a
        self.b = b
        self.c = c
        self.d = d

    def print(self):
        print(f"Cube = {self.a}, Triple = ({self.b}, {self.c}, {self.d})")

    def isPerfectCube(self):
        a3 = pow(self.a, 3)
        b3 = pow(self.b, 3)
        c3 = pow(self.c, 3)
        d3 = pow(self.d, 3)

        if(a3 == b3 + c3 + d3):
            return True
        else:
            return False

    def __eq__(self, value):
        if self.a == value.a:
            thisParams = [self.b, self.c, self.d]
            otherParams = [value.b, value.c, value.d]
            thisParams.sort()
            otherParams.sort()
            if thisParams[0] == otherParams[0] and thisParams[1] == otherParams[1] and thisParams[2] == otherParams[2]:
                return True
        
        return False