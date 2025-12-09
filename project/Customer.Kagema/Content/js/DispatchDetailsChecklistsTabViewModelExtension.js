(function () {
	// Check if base exists
	if (!window.Crm || !window.Crm.Service || !window.Crm.Service.ViewModels || !window.Crm.Service.ViewModels.DispatchDetailsChecklistsTabViewModel) {
		console.error("DispatchDetailsChecklistsTabViewModel base not found - extension cannot load");
		return;
	}

	var baseViewModel = window.Crm.Service.ViewModels.DispatchDetailsChecklistsTabViewModel;
	var basePrototype = baseViewModel.prototype;

	// Store original applyOrderBy if it exists
	var originalApplyOrderBy = basePrototype.applyOrderBy;

	// Add/Override applyOrderBy to sort current job's checklists to top (like Materials/Times tabs)
	basePrototype.applyOrderBy = function (queryable) {
		var viewModel = this;
		
		// First apply original ordering if it exists
		if (originalApplyOrderBy) {
			queryable = originalApplyOrderBy.call(viewModel, queryable);
		}

		// Apply orderByCurrentServiceOrderTime to bring current job's checklists to top
		if (viewModel.currentServiceOrderTimeId) {
			queryable = queryable.orderByCurrentServiceOrderTime(viewModel.currentServiceOrderTimeId);
		}

		return queryable;
	};

	console.log("DispatchDetailsChecklistsTabViewModelExtension loaded successfully");
})();
