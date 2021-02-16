docker rmi munkalap
docker build -t munkalap ./
docker image save --output munkalap.docker.image.tar munkalap
del p:\pali\VM\munkalap.docker.image.tar
move munkalap.docker.image.tar p:\pali\VM
pause
