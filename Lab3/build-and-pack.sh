cd Lab3

echo "Building the project in Release mode..."
dotnet build -c Release

echo "Packing the project using NuGet..."
dotnet pack Lab3.csproj -c Release -o ./nupkg

echo "Build and pack completed successfully."

cd ..
cd Lab3.Runner

echo "Adding package to Lab3.Runner."
dotnet add package DVashchilina --source ../Lab3/nupkg

dotnet restore

echo "Run the project..."
dotnet run