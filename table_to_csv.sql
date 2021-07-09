SET @TABLE = 'transaction';

SET @TS = DATE_FORMAT(NOW(),'_%Y_%m%d_%H_%i');

SET @CMD = CONCAT(
"SELECT * FROM ", @TABLE, 
" INTO OUTFILE '",@TABLE,@TS, 
"' FIELDS ENCLOSED BY '\"' "
"TERMINATED BY ';' "
"ESCAPED BY '\"' "
"LINES TERMINATED BY '\r\n';"
);

PREPARE statement FROM @CMD;
EXECUTE statement;