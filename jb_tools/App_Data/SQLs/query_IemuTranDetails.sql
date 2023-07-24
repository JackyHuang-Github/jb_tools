SELECT 
	IemuTranDetails.Id, IemuTranDetails.No, IemuTranDetails.Seq, 
	IemuTranDetails.MainCode, IemuMainMenus.Name AS MName, 
	IemuTranDetails.ScId, IemuSubMenus.Name AS SName, 
	IemuTranDetails.DetailOrder, 
    IemuTranDetails.Program, 
	IemuDetailMenus.Name AS PName, 
    IemuDetailMenus.Ename AS PEName, 
    IemuDetailMenus.ProgramPath AS ProgramPath, 
	IemuDetailMenus.PosOrPath2 AS PosOrPath2,
	IemuTranDetails.Remark
FROM IemuTranDetails 
LEFT OUTER JOIN IemuMainMenus ON IemuTranDetails.MainCode = IemuMainMenus.MainCode
LEFT OUTER JOIN IemuSubMenus ON IemuTranDetails.MainCode = IemuSubMenus.MainCode AND IemuTranDetails.Scid = IemuSubMenus.ScId 
LEFT OUTER JOIN IemuDetailMenus ON IemuTranDetails.MainCode = IemuDetailMenus.MainCode AND IemuTranDetails.Scid = IemuDetailMenus.ScId 
	AND IemuTranDetails.Program = IemuDetailMenus.Program
