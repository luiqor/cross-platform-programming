# This vagrantfile can be used to create a virtual machine that will host a BaGet server and push a package to it
Vagrant.configure("2") do |config|
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

      wget http://archive.ubuntu.com/ubuntu/pool/main/o/openssl/libssl1.1_1.1.1f-1ubuntu2_amd64.deb
      dpkg -i libssl1.1_1.1.1f-1ubuntu2_amd64.deb

      # Install required libraries for .NET 3.1
      apt-get install -y libicu66

      apt-get install -y dotnet-sdk-8.0
      apt-get install -y dotnet-runtime-3.1 aspnetcore-runtime-3.1

      wget https://github.com/loic-sharma/BaGet/releases/download/v0.4.0-preview2/BaGet.zip -O /tmp/BaGet.zip
      unzip -o /tmp/BaGet.zip -d /vagrant/baget
      rm /tmp/BaGet.zip

      cd /vagrant/baget
      dotnet BaGet.dll --urls "http://*:5001"

      sleep 30

      cd /vagrant
      sudo dotnet pack /vagrant/Lab4/Lab4/Lab4.csproj --configuration Release
      sudo dotnet nuget push -s http://localhost:5001/v3/index.json /vagrant/Lab4/Lab4/nupkg/DVashchilina.1.0.0.nupkg
    SHELL
  end
end