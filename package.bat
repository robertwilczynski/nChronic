pushd .
rmdir /S /Q build
mkdir build

@.\src\.nuget\nuget.exe pack .\nuget\Chronic.nuspec -OutputDirectory .\build -Symbols

@popd
