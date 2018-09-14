FROM microsoft/dotnet:2.1-sdk as build-env
WORKDIR /app

COPY . ./

RUN dotnet publish GradeBook -c Release -o ../out

FROM microsoft/dotnet:2.1-aspnetcore-runtime
WORKDIR /app

COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "GradeBook.dll"]
