class Video:
    def __init__(self, uploader, age, category, length, views, rate, ratings, comments):
        self.uploader = uploader
        self.age = int(age) if age else None
        self.category = category
        self.length = int(length) if length else None
        self.views = int(views) if views else None
        self.rate = float(rate) if rate else None
        self.ratings = int(ratings) if ratings else None
        self.comments = int(comments) if comments else None

    def print(self):
        print(f"{self.uploader} {self.age} {self.category} {self.length} {self.views} {self.rate} {self.ratings} {self.comments}")

    def __str__(self):
        return f"{self.uploader} {self.age} {self.category} {self.length} {self.views} {self.rate} {self.ratings} {self.comments}"

    def ToStringWithCommas(self):
        return f"{self.uploader},{self.age},{self.category},{self.length},{self.views},{self.rate},{self.ratings},{self.comments}"

    def GetAtribute(self, index):
        if index == 0:
            return self.uploader
        elif index == 1:
            return self.age
        elif index == 2:
            return self.category
        elif index == 3:
            return self.length
        elif index == 4:
            return self.views
        elif index == 5:
            return self.rate
        elif index == 6:
            return self.ratings
        elif index == 7:
            return self.comments
        return None