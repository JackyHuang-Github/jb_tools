SELECT *
FROM IemuDetailMenus
WHERE 1 = 1
AND Program IN
(
	SELECT Program
	FROM IemuDetailMenus
	GROUP BY Program
	HAVING COUNT(*) > 1
)
ORDER BY Program, MainCode, ScId
