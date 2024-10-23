Vagrant.configure("2") do |config|
  config.vm.define "ubuntu" do |ubuntu|
    ubuntu.vm.box = "ubuntu/jammy64"
    ubuntu.vm.network "forwarded_port", guest: 5001, host: 8080

    ubuntu.vm.provider "virtualbox" do |vb|
      vb.memory = "8192"
    end

    ubuntu.vm.provision "shell", path: "vagrant-provisions/ubuntu.sh"
  end
end