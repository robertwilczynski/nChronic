set msBuildDir=%WINDIR%\Microsoft.NET\Framework\v4.0.30319
set solutionDir="."
pushd %solutionDir%

call %msBuildDir%\msbuild /v:normal /target:Build /p:Configuration=SignedRelease /p:SignAssembly=true /p:AssemblyOriginatorKeyFile=C:\Users\rober\Dropbox\ultrico\dev\certificates\ultrico.snk src\Chronic.sln

@popd
