SELECT
	c.Id AS CaseId,
	car.[Year], 
	mk.Name AS Make, 
	md.Name AS Model, 
	sm.Name AS SubModel,
	b.Name AS CurrentBuyer,
	cq.Ammount AS CurrentQuote,
	st.Name AS CurentStatus,
	cs.StatusDate
FROM Cases c
	JOIN Cars car			ON car.Id = c.CarId
	JOIN Makes mk			ON mk.Id = car.MakeId
	JOIN Models md			ON md.Id = car.ModelId
	JOIN Submodels sm		ON sm.Id = car.SubmodelId
	JOIN CaseQuotes cq		On cq.CaseId = c.Id AND cq.IsCurrent = 1
	JOIN BuyerZipQuotes z	ON z.Id = cq.BuyerZipQuoteId
	JOIN Buyers b			ON b.Id = z.BuyerId
	JOIN CaseStatus cs		ON cs.CaseId = c.Id AND cs.IsCurrent = 1
	JOIN Statuses st		ON st.Id = cs.StatusId;