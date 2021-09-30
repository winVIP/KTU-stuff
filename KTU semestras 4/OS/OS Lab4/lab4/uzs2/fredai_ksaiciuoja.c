
/*
 * CS170:
 * print4.c -- forks off THREADS threads that print their ids
 */
#include <string.h>
#include <unistd.h>
#include <stdlib.h>
#include <pthread.h>
#include <stdio.h>
#include <sys/types.h>

#define THREADS 4

void *printme(void * arg)
{
	long int i = (long int) arg;
        printf("Hi.  I'm thread %ld	My number = %li\n", pthread_self(), i);
	 return NULL;
}

int main()
{
        long int i;
        void *retval;
        int err;
        pthread_t tid_array[THREADS];


        for (i = 0; i < THREADS; i++)
        {
                err = pthread_create(&(tid_array[i]),  NULL, printme,(void *) i);
		if(err != 0)
                {
                        fprintf(stderr,"thread %li ",i);
                        perror("on create");
                        exit(1);
                }
        }

        printf("main thread -- ");
        printme(NULL);  /* main thread */

        for (i = 0; i < THREADS; i++)
        {
                printf("I'm %ld Trying to join with thread %ld\n",
                       pthread_self() ,tid_array[i]);
                pthread_join(tid_array[i], &retval);
                printf("%ld Joined with thread %ld\n",
                        pthread_self(),tid_array[i]);
        }

        return(0);
}
