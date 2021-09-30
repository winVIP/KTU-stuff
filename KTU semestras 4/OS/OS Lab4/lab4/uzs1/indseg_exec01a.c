#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
int main( int argc, char *argv[] ){

	extern char **environ;

	if(argc!=2){
		perror("Blogas argumentu kiekis");
	}
	int a = atoi(argv[1]);
	char *file[] ={ "./a.out", argv[1], NULL};
	printf("PID: %d\n",  getpid());
	printf("PPID: %d\n",  getppid());
	printf("argv: %d\n",  a);
	a= a-1;

	char *aa[2];
	sprintf(aa, "%d", a);



	if(a > 0){
		execlp("./a.out", (char *)a, (char *)0);
	}
	return 0;
}
//kaip int paverst i char????
