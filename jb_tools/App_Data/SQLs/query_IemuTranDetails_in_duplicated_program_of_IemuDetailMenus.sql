-- 查詢程式代號在 IemuDetailMenus 裡是重複者
SELECT *
FROM IemuTranDetails
WHERE 1 = 1
AND Program IN
(
	SELECT Program
	FROM IemuDetailMenus
	GROUP BY Program
	HAVING COUNT(*) > 1
)
ORDER BY Program, MainCode, ScId, DetailOrder
--ORDER BY Id

/*

-- 查詢程式代號在 IemuDetailMenus 裡是不重複者
SELECT *
FROM IemuTranDetails
WHERE 1 = 1
AND Program NOT IN
(
	SELECT Program
	FROM IemuDetailMenus
	GROUP BY Program
	HAVING COUNT(*) > 1
)
ORDER BY Program, MainCode, ScId, DetailOrder

*/

SELECT Id
FROM IemuTranDetails
WHERE 1 = 1
GROUP BY Id
HAVING COUNT(*) > 1
ORDER BY Id
