#!/bin/bash

cd "$(dirname "$0")"
git status
git add .
git commit -m "Update"
git push