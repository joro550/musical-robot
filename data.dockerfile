FROM postgres:latest
COPY ./dataset/ /docker-entrypoint-initdb.d/


