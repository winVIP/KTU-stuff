
#include <stdlib.h>
#include <stdio.h>
#include <sys/wait.h>
#include <unistd.h>
#include <signal.h>

//jis mega

int main(){

pid_t pr = fork();
printf("parent: %d \n", getpid());
int st;
char command[50];
strcpy( command, "ps -l --forest" );

if(pr == -1){
        perror("can't fork"); return 1;
}
else if(pr != 0){
        printf("child: %d \n", getpid());
        //wait(&st);
	system(command);
	exit(0);
}

return 0;
}


