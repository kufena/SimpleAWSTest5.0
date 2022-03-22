#!/bin/sh

touch /root/App
touch /etc/systemd/system/website.service

rm -rf /root/App/*
rm /etc/systemd/system/website.service