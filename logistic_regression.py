# This program uses Logistic Regression to predict if a tweet about Los Angeles is positive, negative, or neutral

import pandas as pd
import numpy as np
#import pdb

from sklearn.model_selection import train_test_split
from sklearn.linear_model import LogisticRegression
#from sklearn import metrics

DATA_SET_PATH = 'C:/Users/Mae/Desktop/TorobotCSharp/data/mae_test_data.csv'

def dataset_headers(dataset):
    # returns a list of header names
    return list(dataset.columns.values)

def train_logistic_regression(train_wL, train_nL):
    # returns a training model using the training sets
    training_model = LogisticRegression()
    training_model.fit(train_wL, train_nL)
    return training_model

def model_accuracy(trained_model, feature, label):
    # get the accuracy of the training model
    accuracy_score = trained_model.score(feature, label)
    return accuracy_score

def main():
    # Load the data set
    dataset = pd.read_csv(DATA_SET_PATH)
    print("Number of tweets: ", len(dataset))

    # Get the first tweet
    print(dataset.head())

    headers = dataset_headers(dataset)
    print(f"Data set headers: {headers}")

    # Split dataset into training and testing sets
    # wL = with label, nL = no label
    X = np.array(dataset.drop(['Label'],1))
    y = np.array(dataset['Label'])
    train_wL, test_wL, train_nL, test_nL = train_test_split(X, y, train_size=0.7)

    print("train_wL size: ", train_wL.shape)
    print("test_wL size: ", test_wL.shape)
    print("train_nL size: ", train_nL.shape)
    print("test_nL size: ", test_nL.shape)

    # train the logistic regression model
    trained_model = train_logistic_regression(train_wL, train_nL)

    # check the accuracy of the model using the training sets
    training_accuracy = model_accuracy(trained_model, train_wL, train_nL)
    print("Training accuracy: ",  training_accuracy)

    # check the accuracy of the model using the testing sets
    testing_accuracy = model_accuracy(trained_model, test_wL, test_nL)
    print("Testing accuracy: ", testing_accuracy)

if __name__ == "__main__":
    main()





    





