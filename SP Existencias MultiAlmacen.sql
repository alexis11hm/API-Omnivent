CREATE PROCEDURE ObtenerExistenciasAlmacenes
WITH ENCRYPTION
AS
BEGIN
	DECLARE @sql nvarchar(max),
	@nombres nvarchar(max),
	@ids nvarchar(200), 
	@almacenId int,
	@descripcion varchar(40);

	DECLARE infoAlmacen CURSOR LOCAL FAST_FORWARD
	FOR SELECT alm_id, alm_descripcion FROM pdv_almacen WHERE alm_id > 1 AND alm_estatus = 'A'
	ORDER BY alm_descripcion;
	
	SET @sql = NULL;
	OPEN infoAlmacen;
	
	FETCH NEXT FROM infoAlmacen INTO @almacenId, @descripcion;
	WHILE @@FETCH_STATUS = 0
	BEGIN
		IF @ids IS NULL
		BEGIN
			SET @nombres = 'ISNULL([' + CONVERT(varchar(20), @almacenId) + '], 0) AS "' + @descripcion + '"';
			SET @ids = '[' + CONVERT(varchar(20), @almacenId) + ']';
		END
		ELSE
		BEGIN
			SET @nombres = @nombres + ', ISNULL([' + CONVERT(varchar(20), @almacenId) + '], 0) AS "' + @descripcion + '"';
			SET @ids = @ids + ', [' + CONVERT(varchar(20), @almacenId) + ']';
		END;

		FETCH NEXT FROM infoAlmacen INTO @almacenId, @descripcion;
	END;

	CLOSE infoAlmacen;
	DEALLOCATE infoAlmacen;

	SET @sql = 'SELECT pro_id, pro_descripcion, pro_identificacion, ' + @nombres + ', suc_nombre ';
	SET @sql = @sql + 'FROM ';
	SET @sql = @sql + '(SELECT p.pro_id, pro_descripcion, pro_identificacion, ap.alm_id, alp_stock_actual, suc_nombre ';
	SET @sql = @sql + 'FROM glb_producto p ';
	SET @sql = @sql + 'INNER JOIN pdv_almacen_producto ap ON p.pro_id = ap.pro_id ';
	SET @sql = @sql + 'INNER JOIN pdv_almacen a ON ap.alm_id = a.alm_id ';
	SET @sql = @sql + 'INNER JOIN glb_sucursal s ON a.suc_id = s.suc_id) x ';
	SET @sql = @sql + 'PIVOT ';
	SET @sql = @sql + '(SUM(alp_stock_actual) FOR alm_id IN (' + @ids + ')) AS pvt ';
	SET @sql = @sql + 'ORDER BY pvt.pro_descripcion';

	EXECUTE sp_executesql @sql;
END;
GO