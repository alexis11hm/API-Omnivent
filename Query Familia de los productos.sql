select pro_descripcion,familia.fam_descripcion,subfamilia.sub_descripcion 
from GLB_PRODUCTO as producto join GLB_FAMILIA familia on producto.fam_id = familia.fam_id join GLB_SUBFAMILIA subfamilia on producto.fam_id = subfamilia.fam_id;

select pro_id
      ,pro_descripcion
      ,pro_codigo_barras
      ,pro_identificacion
      ,fam_descripcion
      ,sub_descripcion
      ,pro_precio_general_iva
      ,pro_costo_general_iva
from GLB_PRODUCTO as producto join GLB_FAMILIA familia on producto.fam_id = familia.fam_id join GLB_SUBFAMILIA subfamilia on producto.fam_id = subfamilia.fam_id;