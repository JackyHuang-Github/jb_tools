SELECT
	IemuTrans.Id, IemuTrans.No, IemuTrans.Status, IemuTrans.CuNo, iecusuh.cu_na, IemuTrans.CuSale, 
    iepb03h.cu_snam, IemuTrans.IndustryNo, Industries.IndustryName, IemuTrans.Remark
FROM IemuTrans 
LEFT OUTER JOIN iecusuh ON IemuTrans.CuNo = iecusuh.cu_no
LEFT OUTER JOIN iepb03h ON IemuTrans.CuSale = iepb03h.cu_sale 
LEFT OUTER JOIN Industries ON IemuTrans.IndustryNo = Industries.IndustryNo
ORDER BY IemuTrans.No
