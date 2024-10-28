#!/bin/bash

PGUSER="agava"
PGPASSWORD="somestrongpassword"
PGHOST="localhost"
PGDATABASE="demo-webapi"

BACKUP_DIR="/home/DemoWebApi/postgresql-backup"

TIMESTAMP=$(date +"%F_%H-%M-%S")
BACKUP_FILE="$BACKUP_DIR/${PGDATABASE}_$TIMESTAMP.sql"

docker exec -t postgres pg_dump -U $PGUSER -d $PGDATABASE > $BACKUP_FILE

find $BACKUP_DIR -type f -name "*.sql" -mtime +7 -exec rm -f {} \;

echo "Backup completed: $BACKUP_FILE"

# how to restore:
# docker exec -i your_postgres_container_name psql -U $PGUSER -d $PGDATABASE < /path/to/backup_file.sql
