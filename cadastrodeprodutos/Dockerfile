FROM docker.ambevdevs.com.br/hercules-csharp:3.0.1-bionic

WORKDIR /app
COPY /app ./
EXPOSE 80

CMD /bin/sh -c "dotnet CadastroProdutos.WebApi.dll"