#include <stdio.h>
#include <sys/wait.h>
#include <unistd.h>
#include <signal.h>
#include <stdlib.h>
//proseso, kurio tevas pasibaige tevu tampa init



int main( ){
pid_t pr = fork();
printf("*%d \n", getpid());

if(pr == -1){
	perror("can't fork"); return 1;
}
else if(pr != 0){
        printf("*%d \n", getpid());
        exit(0);
}
else{
	printf("killed %d\n", getppid());
	kill(getppid(), 0);
	printf("c: %d \n",getpid());
	printf("p: %d \n",getppid());
	exit(0);
}
return 0;
}
