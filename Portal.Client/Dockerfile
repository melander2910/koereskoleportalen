FROM node:20.12-alpine

WORKDIR /app

COPY "./koereskoleportalen/Portal.Client/package.json" .
#COPY package.json .

RUN npm install

COPY . .

#RUN npm run build

EXPOSE 5173

#CMD [ "npm", "run", "preview", "--", "--host", "0.0.0.0" ]
#CMD [ "npm", "run", "dev" ]
CMD [ "npm", "run", "dev", "--", "--host", "0.0.0.0" ]
