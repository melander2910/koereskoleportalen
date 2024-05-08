// nswag openapi2tsclient /input:https://localhost:7170/swagger/v1/swagger.json /output:backoffice-api.ts
// nswag openapi2tsclient /input:https://localhost:7170/swagger/v1/swagger.json /classname:{controller}Client /output:backoffice-api.ts
// openapi -i https://localhost:7170/swagger/v1/swagger.json -o api -c
// openapi -i swagger.json -o api -c

// npx swagger-typescript-api -p https://localhost:7170/swagger/v1/swagger.json -o -n myApi.ts
// npx swagger-typescript-api -p https://localhost:7170/swagger/v1/swagger.json --modular -o -n myApi.ts

// npx swagger-typescript-api -p https://localhost:7170/swagger/v1/swagger.json --route-types

// npx swagger-typescript-api -p https://localhost:7170/swagger/v1/swagger.json --no-client





// Most recent generations

// npx swagger-typescript-api -p https://localhost:7170/swagger/v1/swagger.json -o -n backoffice-api.ts
// npx swagger-typescript-api -p https://localhost:7082/swagger/v1/swagger.json -o -n auth-api.ts


// npx swagger-typescript-api -p http://localhost:5199/swagger/v1/swagger.json -o -n backoffice-api.ts
// npx react-query-swagger /input:http://localhost:5199/swagger/v1/swagger.json /output:api-client.ts /template:Fetch