#Stage 1
FROM node:18-alpine as build-step

EXPOSE 80

RUN mkdir -p /app

WORKDIR /app

COPY package.json /app

RUN npm install

COPY . /app

RUN npm run build --prod
# Stage 2
FROM nginx:1.22.0-alpine

COPY --from=build-step /app/dist/furnitureorder /usr/share/nginx/html

ENTRYPOINT ["ng"]


