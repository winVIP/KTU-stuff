import video
import funkcijos
import plotly.graph_objects as go
import math

dataList = list()

eUploader = 0
eAge = 0
eCategory = 0
eLength = 0
eViews = 0
eRate = 0
eRatings = 0
eComments = 0

atributeNames = list()
missingValues = list()
avarages = list()
medians = list()
mins = list()
maxs = list()
cardinalities = list()
quartiles1st = list()
quartiles2nd = list()
standartDeviations = list()

atributeNames2 = list()
missingValues2 = list()
cardinalities2 = list()
modes2nd = list()
modeRates = list()
modePercents = list()
modes = list()
mode2ndRates = list()
mode2ndPercents = list()

def ReadData():
    dataFile = open("Good data with commas.csv", "r")

    dataRL = dataFile.readlines()

    for x in dataRL:

        wordList = x.split(",")

        #print(f"{wordList[0]}|{wordList[1]}|{wordList[2]}|{wordList[3]}|{wordList[4]}|{wordList[5]}|{wordList[6]}|{wordList[7]}")

        global eUploader
        if not wordList[0]: eUploader += 1
        global eAge
        if not wordList[1]: eAge += 1
        global eCategory
        if not wordList[2]: eCategory += 1
        global eLength
        if not wordList[3]: eLength += 1
        global eViews
        if not wordList[4]: eViews += 1
        global eRate
        if not wordList[5]: eRate += 1
        global eRatings
        if not wordList[6]: eRatings += 1
        global eComments
        if not wordList[7]: eComments += 1

        dataList.append(video.Video(wordList[0], wordList[1], wordList[2], wordList[3], wordList[4], wordList[5], wordList[6], wordList[7]))

    dataFile.close()

ReadData()

def ProcessAge():
    global atributeNames
    global missingValues
    global avarages
    global medians
    global mins
    global maxs
    global cardinalities
    global quartiles1st
    global quartiles2nd
    global standartDeviations

    valueList = list()
    valueSet = set()

    for x in dataList:
        if x.age is not None:
            valueList.append(x.age)
            valueSet.add(x.age)

    atributeNames.append('Age')
    missingValues.append(100 / (len(valueList) + eAge) * eAge)
    avarages.append(funkcijos.Funkcijos.Vidurkis(None, valueList))
    medians.append(funkcijos.Funkcijos.Mediana(None, valueList))
    mins.append(funkcijos.Funkcijos.Minimum(None, valueList))
    maxs.append(funkcijos.Funkcijos.Maximum(None, valueList))
    cardinalities.append(len(valueSet))
    quartiles = funkcijos.Funkcijos.Quartiles(None, valueList)
    quartiles1st.append(quartiles[0])
    quartiles2nd.append(quartiles[1])
    standartDeviations.append(funkcijos.Funkcijos.StandartDeviation(None,valueList))

def ProcessLength():
    global atributeNames
    global missingValues
    global avarages
    global medians
    global mins
    global maxs
    global cardinalities
    global quartiles1st
    global quartiles2nd
    global standartDeviations

    valueList = list()
    valueSet = set()

    for x in dataList:
        if x.length is not None:
            valueList.append(x.length)
            valueSet.add(x.length)

    atributeNames.append('Length')
    missingValues.append(100 / (len(valueList) + eLength) * eLength)
    avarages.append(funkcijos.Funkcijos.Vidurkis(None, valueList))
    medians.append(funkcijos.Funkcijos.Mediana(None, valueList))
    mins.append(funkcijos.Funkcijos.Minimum(None, valueList))
    maxs.append(funkcijos.Funkcijos.Maximum(None, valueList))
    cardinalities.append(len(valueSet))
    quartiles = funkcijos.Funkcijos.Quartiles(None, valueList)
    quartiles1st.append(quartiles[0])
    quartiles2nd.append(quartiles[1])
    standartDeviations.append(funkcijos.Funkcijos.StandartDeviation(None,valueList))

def ProcessViews():
    global atributeNames
    global missingValues
    global avarages
    global medians
    global mins
    global maxs
    global cardinalities
    global quartiles1st
    global quartiles2nd
    global standartDeviations

    valueList = list()
    valueSet = set()

    for x in dataList:
        if x.views is not None:
            valueList.append(x.views)
            valueSet.add(x.views)

    atributeNames.append('Views')
    missingValues.append(100 / (len(valueList) + eViews) * eViews)
    avarages.append(funkcijos.Funkcijos.Vidurkis(None, valueList))
    medians.append(funkcijos.Funkcijos.Mediana(None, valueList))
    mins.append(funkcijos.Funkcijos.Minimum(None, valueList))
    maxs.append(funkcijos.Funkcijos.Maximum(None, valueList))
    cardinalities.append(len(valueSet))
    quartiles = funkcijos.Funkcijos.Quartiles(None, valueList)
    quartiles1st.append(quartiles[0])
    quartiles2nd.append(quartiles[1])
    standartDeviations.append(funkcijos.Funkcijos.StandartDeviation(None,valueList))

def ProcessRate():
    global atributeNames
    global missingValues
    global avarages
    global medians
    global mins
    global maxs
    global cardinalities
    global quartiles1st
    global quartiles2nd
    global standartDeviations

    valueList = list()
    valueSet = set()

    for x in dataList:
        if x.rate is not None:
            valueList.append(x.rate)
            valueSet.add(x.rate)

    atributeNames.append('Rate')
    missingValues.append(100 / (len(valueList) + eRate) * eRate)
    avarages.append(funkcijos.Funkcijos.Vidurkis(None, valueList))
    medians.append(funkcijos.Funkcijos.Mediana(None, valueList))
    mins.append(funkcijos.Funkcijos.Minimum(None, valueList))
    maxs.append(funkcijos.Funkcijos.Maximum(None, valueList))
    cardinalities.append(len(valueSet))
    quartiles = funkcijos.Funkcijos.Quartiles(None, valueList)
    quartiles1st.append(quartiles[0])
    quartiles2nd.append(quartiles[1])
    standartDeviations.append(funkcijos.Funkcijos.StandartDeviation(None,valueList))

def ProcessRatings():
    global atributeNames
    global missingValues
    global avarages
    global medians
    global mins
    global maxs
    global cardinalities
    global quartiles1st
    global quartiles2nd
    global standartDeviations

    valueList = list()
    valueSet = set()

    for x in dataList:
        if x.ratings is not None:
            valueList.append(x.ratings)
            valueSet.add(x.ratings)

    atributeNames.append('Ratings')
    missingValues.append(100 / (len(valueList) + eRatings) * eRatings)
    avarages.append(funkcijos.Funkcijos.Vidurkis(None, valueList))
    medians.append(funkcijos.Funkcijos.Mediana(None, valueList))
    mins.append(funkcijos.Funkcijos.Minimum(None, valueList))
    maxs.append(funkcijos.Funkcijos.Maximum(None, valueList))
    cardinalities.append(len(valueSet))
    quartiles = funkcijos.Funkcijos.Quartiles(None, valueList)
    quartiles1st.append(quartiles[0])
    quartiles2nd.append(quartiles[1])
    standartDeviations.append(funkcijos.Funkcijos.StandartDeviation(None,valueList))

def ProcessComments():
    global atributeNames
    global missingValues
    global avarages
    global medians
    global mins
    global maxs
    global cardinalities
    global quartiles1st
    global quartiles2nd
    global standartDeviations

    valueList = list()
    valueSet = set()

    for x in dataList:
        if x.comments is not None:
            valueList.append(x.comments)
            valueSet.add(x.comments)

    atributeNames.append('Comments')
    missingValues.append(100 / (len(valueList) + eComments) * eComments)
    avarages.append(funkcijos.Funkcijos.Vidurkis(None, valueList))
    medians.append(funkcijos.Funkcijos.Mediana(None, valueList))
    mins.append(funkcijos.Funkcijos.Minimum(None, valueList))
    maxs.append(funkcijos.Funkcijos.Maximum(None, valueList))
    cardinalities.append(len(valueSet))
    quartiles = funkcijos.Funkcijos.Quartiles(None, valueList)
    quartiles1st.append(quartiles[0])
    quartiles2nd.append(quartiles[1])
    standartDeviations.append(funkcijos.Funkcijos.StandartDeviation(None,valueList))

def ProcessUploader():
    global atributeNames2
    global missingValues2
    global cardinalities2
    global modes
    global modeRates
    global modePercents
    global modes2nd
    global mode2ndRates
    global mode2ndPercents

    valueList = list()
    valueSet = set()

    for x in dataList:
        if x.uploader is not None:
            valueList.append(x.uploader)
            valueSet.add(x.uploader)

    atributeNames2.append("Uploader")
    missingValues2.append(100 / (len(valueList) + eUploader) * eUploader)
    cardinalities2.append(len(valueSet))
    modes.append(funkcijos.Funkcijos.Moda(None, valueList))
    modeRates.append(valueList.count(funkcijos.Funkcijos.Moda(None, valueList)))
    modePercents.append(100 / len(valueList) * valueList.count(funkcijos.Funkcijos.Moda(None, valueList)))
    modes2nd.append(funkcijos.Funkcijos.Moda2nd(None, valueList))
    mode2ndRates.append(valueList.count(funkcijos.Funkcijos.Moda2nd(None, valueList)))
    mode2ndPercents.append(100 / len(valueList) * valueList.count(funkcijos.Funkcijos.Moda2nd(None, valueList)))

def ProcessCategory():
    global atributeNames2
    global missingValues2
    global cardinalities2
    global modes
    global modeRates
    global modePercents
    global modes2nd
    global mode2ndRates
    global mode2ndPercents

    valueList = list()
    valueSet = set()

    for x in dataList:
        if x.category is not None:
            valueList.append(x.category)
            valueSet.add(x.category)

    atributeNames2.append("Category")
    missingValues2.append(100 / (len(valueList) + eCategory) * eCategory)
    cardinalities2.append(len(valueSet))
    modes.append(funkcijos.Funkcijos.Moda(None, valueList))
    modeRates.append(valueList.count(funkcijos.Funkcijos.Moda(None, valueList)))
    modePercents.append(100 / len(valueList) * valueList.count(funkcijos.Funkcijos.Moda(None, valueList)))
    modes2nd.append(funkcijos.Funkcijos.Moda2nd(None, valueList))
    mode2ndRates.append(valueList.count(funkcijos.Funkcijos.Moda2nd(None, valueList)))
    mode2ndPercents.append(100 / len(valueList) * valueList.count(funkcijos.Funkcijos.Moda2nd(None, valueList)))

ProcessAge()
ProcessLength()
ProcessViews()
ProcessRate()
ProcessRatings()
ProcessComments()

fig = go.Figure(
    data=[go.Table(header=dict(values=['Atributas', 'Trukstamos reiksmes %', 'Vidurkis','Mediana','Minimumas','Maximumas','Kardinalumas','Pirmas kvartilis','Trecias Kvartilis', 'Standartinis nuokrypis']),
cells=dict( values=[ atributeNames, [ round(e, 2) for e in missingValues ], [ round(e, 2) for e in avarages ], medians, mins, maxs, cardinalities, quartiles1st, quartiles2nd, [ round(e, 2) for e in standartDeviations ] ] ))
])

ProcessUploader()
ProcessCategory()

fig2 = go.Figure(
    data=[go.Table(header=dict(values=['Atributas', 'Trukstamos reiksmes %', 'Kardinalumas','Moda','Modos Daznis','Moda %','2-oji Moda','2-osios Modos Daznis','2-oji Moda %']),
cells=dict( values=[ atributeNames2, [ round(e, 2) for e in missingValues2 ], cardinalities2, modes, modeRates, [ round(e, 2) for e in modePercents ], modes2nd, mode2ndRates, [ round(e, 2) for e in mode2ndPercents ] ] ))
])

fig.write_html('Breziniai\Tolydieji_duomenys.html', auto_open=True)
fig2.write_html('Breziniai\Kategoriniai_duomenys.html', auto_open=True)

def drawHists():
    global dataList

    valueList = list()

    for x in dataList:
        if x.age is not None:
            valueList.append(x.age)

    HistAge = go.Figure(data=[go.Histogram(x=valueList)])
    HistAge.update_layout(xaxis_title_text='Amzius', yaxis_title_text='Kiekis')
    HistAge.write_html('Breziniai\Amziaus_Hist.html', auto_open=True)

    valueList = list()

    for x in dataList:
        if x.length is not None:
            valueList.append(x.length)

    HistLength = go.Figure(data=[go.Histogram(x=valueList)])
    HistLength.update_layout(xaxis_title_text='Ilgis', yaxis_title_text='Kiekis')
    HistLength.write_html('Breziniai\Ilgis_Hist.html', auto_open=True)

    valueList = list()

    for x in dataList:
        if x.views is not None:
            valueList.append(x.views)

    HistViews = go.Figure(data=[go.Histogram(x=valueList)])
    HistViews.update_layout(xaxis_title_text='Perziuros', yaxis_title_text='Kiekis')
    HistViews.write_html('Breziniai\Perziuros_Hist.html', auto_open=True)

    valueList = list()

    for x in dataList:
        if x.rate is not None:
            valueList.append(x.rate)

    HistRate = go.Figure(data=[go.Histogram(x=valueList)])
    HistRate.update_layout(xaxis_title_text='Rate', yaxis_title_text='Kiekis')
    HistRate.write_html('Breziniai\Rate_Hist.html', auto_open=True)

    valueList = list()

    for x in dataList:
        if x.ratings is not None:
            valueList.append(x.ratings)

    HistRatings = go.Figure(data=[go.Histogram(x=valueList)])
    HistRatings.update_layout(xaxis_title_text='Reitingai', yaxis_title_text='Kiekis')
    HistRatings.write_html('Breziniai\Reitingai_Hist.html', auto_open=True)

    valueList = list()

    for x in dataList:
        if x.comments is not None:
            valueList.append(x.comments)

    HistComments = go.Figure(data=[go.Histogram(x=valueList)])
    HistComments.update_layout(xaxis_title_text='Komentarai', yaxis_title_text='Kiekis')
    HistComments.write_html('Breziniai\Komentarai_Hist.html', auto_open=True)

def scatterAndSplom():
    global dataList

    ageList = list()
    lengthList = list()
    viewsList = list()
    rateList = list()
    ratingsList = list()
    commentsList = list()

    for x in dataList:
        if x.age is not None:
            ageList.append(x.age)
        if x.length is not None:
            lengthList.append(x.length)
        if x.views is not None:
            viewsList.append(x.views)
        if x.rate is not None:
            rateList.append(x.rate)
        if x.ratings is not None:
            ratingsList.append(x.ratings)
        if x.comments is not None:
            commentsList.append(x.comments)



    Splom=go.Splom(dimensions=[dict(label='Amzius', values=ageList),
                                dict(label='Ilgis', values=lengthList),
                                dict(label='Perziuros', values=viewsList),
                                dict(label='Rate', values=rateList),
                                dict(label='Reitingai', values=ratingsList),
                                dict(label='Komentarai', values=commentsList)])

    SplomFig = go.Figure(data=Splom)
    SplomFig.write_html('Breziniai\Splom.html', auto_open=True)

    ScatterFigKor1 = go.Figure(data=go.Scatter(x=lengthList, y=rateList, mode="markers"))
    ScatterFigKor1.update_layout(xaxis_title_text='Ilgis', yaxis_title_text='Ivertinimas')
    ScatterFigKor1.write_html('Breziniai\Scatter_Length_Rate_Kor.html', auto_open=True)

    ScatterFigKor2 = go.Figure(data=go.Scatter(x=ageList, y=rateList, mode="markers"))
    ScatterFigKor2.update_layout(xaxis_title_text='Amzius', yaxis_title_text='Ivertinimas')
    ScatterFigKor2.write_html('Breziniai\Scatter_Age_Rate_Kor.html', auto_open=True)

    ScatterFigNekor1 = go.Figure(data=go.Scatter(x=rateList, y=commentsList, mode="markers"))
    ScatterFigNekor1.update_layout(xaxis_title_text='Ivertinimas', yaxis_title_text='Komentarai')
    ScatterFigNekor1.write_html('Breziniai\Scatter_Rate_Comments_Nekor.html', auto_open=True)

    ScatterFigNekor2 = go.Figure(data=go.Scatter(x=rateList, y=ratingsList, mode="markers"))
    ScatterFigNekor2.update_layout(xaxis_title_text='Ivertinimas', yaxis_title_text='Ivertinimu kiekis')
    ScatterFigNekor2.write_html('Breziniai\Scatter_Rate_Ratings_Nekor.html', auto_open=True)

def barPlots():
    global dataList
    categoriesList = list()
    categoriesCount = list()
    for x in dataList:
        if categoriesList.__contains__(x.category):
            categoriesCount[categoriesList.index(x.category)] += x.views
        else:
            categoriesList.append(x.category)
            categoriesCount.append(x.views)

    barCategoriesViewsFig = go.Figure([go.Bar(x=categoriesList, y=categoriesCount)])
    barCategoriesViewsFig.update_layout(xaxis_title_text='Kategorija', yaxis_title_text='Perziuru kiekis')
    barCategoriesViewsFig.write_html('Breziniai\\bar_Cat_Views.html', auto_open=True)

    categoriesList = list()
    categoriesCount = list()
    for x in dataList:
        if categoriesList.__contains__(x.category):
            categoriesCount[categoriesList.index(x.category)] += x.views
        else:
            categoriesList.append(x.category)
            categoriesCount.append(x.rate)

    barCategoriesRatesFig = go.Figure([go.Bar(x=categoriesList, y=categoriesCount)])
    barCategoriesRatesFig.update_layout(xaxis_title_text='Kategorija', yaxis_title_text='Ivertinmu suma')
    barCategoriesRatesFig.write_html('Breziniai\\bar_Cat_Rates.html', auto_open=True)

def boxPlots():
    global dataList
    categoriesList = list()
    categoriesCount = list()
    for x in dataList:
        if categoriesList.__contains__(x.category):
            categoriesCount[categoriesList.index(x.category)].append(x.views)
        else:
            categoriesList.append(x.category)
            newList = list()
            newList.append(x.views)
            categoriesCount.append(newList)

    boxCategoriesViewsFig = go.Figure()
    for x in range(len(categoriesCount) - 1):
        boxCategoriesViewsFig.add_trace(go.Box(y=categoriesCount[x], name=categoriesList[x]))
    boxCategoriesViewsFig.write_html('Breziniai\\box_Cat_Views.html', auto_open=True)

    categoriesList = list()
    categoriesCount = list()
    for x in dataList:
        if categoriesList.__contains__(x.category):
            categoriesCount[categoriesList.index(x.category)].append(x.rate)
        else:
            categoriesList.append(x.category)
            newList = list()
            newList.append(x.rate)
            categoriesCount.append(newList)

    boxCategoriesViewsFig = go.Figure()
    for x in range(len(categoriesCount) - 1):
        boxCategoriesViewsFig.add_trace(go.Box(y=categoriesCount[x], name=categoriesList[x]))
    boxCategoriesViewsFig.write_html('Breziniai\\box_Cat_Rate.html', auto_open=True)

drawHists()
scatterAndSplom()
barPlots()
boxPlots()

forCov = []
covAverages = []
covAverages.append(avarages[0])
covAverages.append(avarages[1])
covAverages.append(avarages[2])
covAverages.append(avarages[3])
covAverages.append(avarages[4])
covAverages.append(avarages[5])
for x in dataList:
    forCov.append([x.age, x.length, x.views, x.rate, x.ratings, x.comments])

cov = []
for i in range(0, 6):
    for j in range(0, 6):
        sum = 0.0
        for x in forCov:
            if x[i] == None or x[j] == None:
                continue
            else:
                sum = sum + ((x[i] - covAverages[i]) * (x[j] - covAverages[j]))
        if j == 0:
            cov.append([sum / (len(forCov) - 1)])
        else:
            cov[i].append(sum / (len(forCov) - 1))

atributeNamesWithoutSpace = atributeNames.copy()
atributeNames.insert(0," ")

fig3 = go.Figure(
    data=[go.Table(header=dict(values=atributeNames),
cells=dict( values=[ atributeNamesWithoutSpace, cov[0], cov[1], cov[2], cov[3], cov[4], cov[5]] ))
])

fig3.write_html('Breziniai\\Kovariacijos lentele.html', auto_open=True)


cor = []
for i in range(0, 6):
    for j in range(0, 6):
        if j == 0:
            cor.append([cov[i][j] / (standartDeviations[i] * standartDeviations[j])])
        else:
            cor[i].append(cov[i][j] / (standartDeviations[i] * standartDeviations[j]))

fig4 = go.Figure(
    data=[go.Table(header=dict(values=atributeNames),
cells=dict( values=[ atributeNamesWithoutSpace, cor[0], cor[1], cor[2], cor[3], cor[4], cor[5]] ))
])

fig4.write_html('Breziniai\\Koreliacijos lentele.html', auto_open=True)

fig5 = go.Figure(data=go.Heatmap(z=cor, x = atributeNamesWithoutSpace, y = atributeNamesWithoutSpace))
fig5.write_html('Breziniai\\Koreliacijos matrica.html', auto_open=True)

NormalizedFile = open("Normalized data.csv", "w+")
NormalizedList = list()

for x in dataList:
    nAge = (x.age - avarages[0]) / standartDeviations[0]
    nLength = (x.length - avarages[1]) / standartDeviations[1]
    nViews = (x.views - avarages[2]) / standartDeviations[2]
    nRate = (x.rate - avarages[3]) / standartDeviations[3]
    nRatings = (x.ratings - avarages[4]) / standartDeviations[4]
    nComments = (x.comments - avarages[5]) / standartDeviations[5]
    NormalizedList.append(video.Video(x.uploader, nAge, x.category, nLength, nViews, nRate, nRatings, nComments))

for x in NormalizedList:
    NormalizedFile.write(x.ToStringWithCommas() + "\n")