@echo off
rem Gera um unico AgendaFestas.exe na pasta "publicado" (nao precisa instalar .NET no computador do cliente)
dotnet publish AgendaFestas.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -p:EnableCompressionInSingleFile=true -o publicado
echo.
echo Pronto! Copie a pasta "publicado" para o computador do cliente e abra o AgendaFestas.exe
pause
