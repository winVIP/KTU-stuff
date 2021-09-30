#include<stdio.h>
#include<stdlib.h>
#include<unistd.h>
#include<sys/types.h>
#include<string.h>
#include<sys/wait.h>

int main( int argc, char *argv[]){

	if(argc != 2){
		printf("Netinkamas argumentu kiekis");
		exit(1);
	}

	int fd[2];
	pid_t pid;

	if(pipe(fd) == -1){
		fprintf( stderr, "Nepavyko sukurti programinio kanalo !\n" );
		exit( 1 );
	}

	pid = fork();

	if( pid < 0){
		fprintf(stderr, "fork Failed" );
		exit(1);
	}
	else if(  pid > 0 ){ //parent
		write( fd[1], argv[1], sizeof( argv[1] ));
		 printf("(tevas) Mano PID: %d\n", getpid() );
	}
	else{
		char text[100];
		printf ( "(vaikas) Tevo proceso ID: %d\n", getpid() );
		read( fd[0], text, sizeof( text ));
		printf("%s", text);
	}
	return 0;
}
