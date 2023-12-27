#!/bin/bash
set -e

echo "Deployment started..."

# Clone the repository
git clone https://github.com/KalashTechnologies/COSYS_FE.git
cd COSYS_FE

echo "Installing Dependencies..."
yarn install

echo "Creating Production Build..."
yarn deploy

echo "Cleaning up..."
export SUDO_ASKPASS=${HOME}/mypass.sh

sudo -A rm -rf /var/www/financefrontend
sudo -A mv /home/app/financefrontend-runner/_work/COSYS_FE/COSYS_FE/COSYS_FE/dist /var/www/financefrontend
sudo -A systemctl restart nginx

echo "Deployment Finished!"