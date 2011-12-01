pushd .
@cd nuget
@..\src\.nuget\nuget.exe pack Chronic.nuspec -OutputDirectory ..\build -Symbols

@popd