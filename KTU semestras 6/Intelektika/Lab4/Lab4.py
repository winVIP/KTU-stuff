import matplotlib.pyplot as plt
from sklearn.cluster import KMeans
import numpy as np
import pandas as pd

def Clusters(fileData, columnNames):
    for cluster in np.arange(2, 5):
        for data, label in zip(fileData, columnNames):
            k_means = KMeans(n_clusters=cluster)
            k_means.fit(data)
            y_k_means = k_means.predict(data)
            plt.scatter(data[:, 0], data[:, 1], c=y_k_means, s=30, cmap='plasma', alpha=0.5)
            k_centers = k_means.cluster_centers_
            plt.scatter(k_centers[:, 0], k_centers[:, 1], c='black', s=100)
            plt.xlabel(label[0], fontsize=12)
            plt.ylabel(label[1], fontsize=12)
            plt.title(label[0] + ' and ' + label[1] + ' when there are ' + str(cluster) + ' cluster\'s ', fontsize=12)
            plt.show()

def main():
    file = pd.read_csv("Lab4_data.csv")
    file = file[['Age', 'Length', 'Views', 'Rating', 'Ratings', 'Comments']]

    print(file.head())

    data = [
        file[['Age', 'Length']].to_numpy(),
        file[['Views', 'Rating']].to_numpy(),
        file[['Ratings', 'Comments']].to_numpy(),
    ]

    columnNames = [
        ['Age', 'Length'],
        ['Views', 'Rating'],
        ['Ratings', 'Comments']
    ]
    Clusters(data, columnNames)

main()