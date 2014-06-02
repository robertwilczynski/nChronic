set msBuildDir=%WINDIR%\Microsoft.NET\Framework\v4.0.30319
set solutionDir="."
pushd %solutionDir%

call %msBuildDir%\msbuild /v:normal /target:Build /p:Configuration=%1 src\Chronic.sln

@popd
