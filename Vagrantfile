Vagrant.configure("2") do |config|
  # Lab4 VM ubuntu that runs commands from the package created from Lab4
  config.vm.define "lab4_ubuntu" do |lab4_ubuntu|
    lab4_ubuntu.vm.box = "ubuntu/jammy64"
    lab4_ubuntu.vm.network "public_network"

    lab4_ubuntu.vm.provider "virtualbox" do |vb|
      vb.memory = "4096"
    end

    lab4_ubuntu.vm.provision "shell", inline: <<-SHELL
      sudo apt-get update
      
      apt-get install -y dotnet-sdk-8.0

      dotnet nuget locals all --clear
      rm -rf /vagrant/nupkg

      cd /vagrant
      sudo dotnet pack /vagrant/Lab4/Lab4/Lab4.csproj --configuration Release --output /vagrant/nupkg
      ls -l /vagrant/nupkg/*.nupkg

      rm -rf /vagrant/App

      dotnet new console -n App
      cd /vagrant/App
      dotnet add package McMaster.Extensions.CommandLineUtils --version 4.1.1
      dotnet add reference /vagrant/LabLibrary/LabLibrary.csproj
      dotnet add package DVashchilina --version 1.0.0 --source /vagrant/nupkg

      cat <<EOL > /vagrant/App/Program.cs
using System;
using Lab4;

namespace App
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("App is running successfully...");
            Lab4.Program.Main(args);
        }
    }
}
EOL

      dotnet build
      dotnet run
      dotnet run -- version

      dotnet run -- run lab1 --input /vagrant/Lab1/input.txt
      echo "Lab1 output:"
      cat /root/output.txt
    SHELL
  end

  # VM to push the Lab4 to the BaGet server
  config.vm.define "packager" do |packager|
    packager.vm.box = "ubuntu/jammy64"
    packager.vm.network "forwarded_port", guest: 5001, host: 5000

    packager.vm.provider "virtualbox" do |vb|
      vb.memory = "8192"
    end

    packager.vm.provision "shell", inline: <<-SHELL
      wget https://packages.microsoft.com/config/ubuntu/22.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
      dpkg -i packages-microsoft-prod.deb
      apt-get update
      apt-get install -y unzip wget curl

      wget http://archive.ubuntu.com/ubuntu/pool/main/o/openssl/libssl1.1_1.1.1f-1ubuntu2_amd64.deb
      dpkg -i libssl1.1_1.1.1f-1ubuntu2_amd64.deb

      # Install required libraries for .NET 3.1 in order for BaGet to work
      apt-get install -y libicu66

      apt-get install -y dotnet-sdk-8.0
      apt-get install -y dotnet-runtime-3.1 aspnetcore-runtime-3.1

      wget https://github.com/loic-sharma/BaGet/releases/download/v0.4.0-preview2/BaGet.zip -O /tmp/BaGet.zip
      unzip -o /tmp/BaGet.zip -d /vagrant/baget
      rm /tmp/BaGet.zip

      cd /vagrant/baget
      dotnet BaGet.dll --urls "http://*:5001"

      cd /vagrant
      sudo dotnet pack /vagrant/Lab4/Lab4/Lab4.csproj --configuration Release
      ls -l /vagrant/Lab4/Lab4/nupkg/*.nupkg
      sudo dotnet nuget push -s http://localhost:5001/v3/index.json /vagrant/Lab4/Lab4/nupkg/DVashchilina.1.0.0.nupkg
    SHELL
  end
end

