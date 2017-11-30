# This program uses Logistic Regression to predict if a tweet about earthquakes is verified or unverified

import pandas as pd
import numpy as np
import pdb
import seaborn as sbn
import matplotlib.pyplot as plt

from sklearn.cross_validation import train_test_split
from sklearn.linear_model import LogisticRegression
from sklearn import metrics

DATA_SET_PATH = #"enter path here (include " ")"

def dataset_headers(dataset):
    # returns a list of header names
    return list(dataset.columns.values)


def main():
    # Load the data set
    dataset = pd.read_csv(DATA_SET_PATH)
    print("Number of tweets: ", len(dataset))

    # Get the first tweet
    print(dataset.head())

    headers = dataset_headers(dataset)
    print(f"Data set headers: {headers}")

    feature = 'Keyword'
    label = 'Label'

    





