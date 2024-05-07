FROM node:18-alpine

WORKDIR /app

COPY ["./koereskoleportalen/Portal.Client/package*.json", "."]

RUN npm install

COPY "./koereskoleportalen/Portal.Client/" .

RUN npm run build

EXPOSE 5173

CMD [ "npm", "run", "preview", "--", "--host", "0.0.0.0" ]
#CMD [ "npm", "run", "dev", "--", "--host", "0.0.0.0" ]