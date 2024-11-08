ARG NODE_VERSION=22
FROM node:${NODE_VERSION} AS build

# Instale dependências necessárias
RUN apt-get update && apt-get install -y curl

# Defina o diretório de trabalho
WORKDIR /app

# Copie package.json e package-lock.json
COPY package*.json ./

# Instale dependências de produção
RUN npm install

# Copie apenas os arquivos necessários
COPY . .

# Segundo estágio - runtime
FROM node:${NODE_VERSION} AS runtime

# Defina o diretório de trabalho
WORKDIR /app

# Copie node_modules e os arquivos de build da camada anterior
COPY --from=build /app .

# Defina a variável de ambiente para produção
ENV NODE_ENV=production

# Exponha a porta 3000
EXPOSE 3000

# Comando de inicialização
CMD ["npm", "start"]
