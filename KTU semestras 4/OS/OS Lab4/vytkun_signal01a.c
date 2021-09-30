/* Vytenis Kunickas IFF-7/2 vytkun */
/* Failas: vytkun_signal01.c */
/*                                                                        */
/* inglagz_signal01: tevo procesas sukuria vaiko procesa                */
/* ir laukia vaiko proceso darbo pabaigos signalo                        */
/* Jo sulaukes darba baigia pats s                                       */
/* Vaiko procesas isspausdina pranesima ir palaukes 3s baigia darba         */
#include <stdlib.h>        
#include <unistd.h>        
#include <signal.h>        
#include <stdio.h>        
#include <sys/wait.h>
void il_catch_CHLD(int);        /* signalo apdorojimo f-ja */
void il_child(void);                /* vaiko proceso veiksmai */
void il_parent(int pid);        /* tevo proceso veiksmai */

void il_child(void) {
    printf("        child: I'm the child\n");
    sleep(3);
    printf("        child: I'm exiting\n");
    kill(getppid(), SIGALRM);
	exit(123);
}
void il_parent(int pid) {
    printf("parent: I'm the parent\n");
    sleep(10);
    printf("parent: exiting\n");
}
void il_catch_ALRM(int snum) {
    int pid;
    int status;
    pid = wait(&status);
    printf("parent: child process (PID=%d) exited with value %d Signal number: %d\n", pid, WEXITSTATUS(status), snum);
}
int main(int argc, char **argv) {
    int pid;                                /* proceso ID */
    printf( "(C) 2019 Vytenis Kunickas, %s\n", __FILE__ );
    signal(SIGALRM, il_catch_ALRM);        /* aptikti vaiko proc pasibaigima ir apdoroti */
    switch (pid = fork()) {
    case 0:                                /* fork() grazina 0 vaiko procesui */
        il_child();
        break;
    default:                                /* fork() grazina vaiko PID tevo procesui */
        il_parent(pid);
        break;
    case -1:                                /* fork() nepavyko */
        perror("fork");
        exit(1);
    }
    exit(0);
}