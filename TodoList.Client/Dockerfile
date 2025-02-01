FROM node:20 AS build
WORKDIR /src
COPY TodoList.Client/package*.json ./
RUN npm install
COPY TodoList.Client/ .
RUN npm run build

