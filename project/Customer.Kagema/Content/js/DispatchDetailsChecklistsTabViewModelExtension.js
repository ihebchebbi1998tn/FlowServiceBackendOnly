(function () {
	// Check if base exists
	if (!window.Crm || !window.Crm.Service || !window.Crm.Service.ViewModels || !window.Crm.Service.ViewModels.DispatchDetailsChecklistsTabViewModel) {
		console.error("DispatchDetailsChecklistsTabViewModel base not found - extension cannot load");
		return;
	}

	var basePrototype = window.Crm.Service.ViewModels.DispatchDetailsChecklistsTabViewModel.prototype;

	// Add applyOrderBy to prioritize checklists for the currently selected job
	basePrototype.applyOrderBy = function (query) {
		var viewModel = this;
		var currentServiceOrderTimeId = null;

		// Get the current ServiceOrderTime ID from the dispatch
		if (viewModel.dispatch && viewModel.dispatch()) {
			var dispatch = viewModel.dispatch();
			if (dispatch.CurrentServiceOrderTimeId) {
				currentServiceOrderTimeId = dispatch.CurrentServiceOrderTimeId();
			}
		}

		// Apply ordering to prioritize current job's checklists at the top
		if (currentServiceOrderTimeId) {
			query = query.orderByDescending("orderByCurrentServiceOrderTime", { currentServiceOrderTimeId: currentServiceOrderTimeId });
		}

		return window.Main.ViewModels.GenericListViewModel.prototype.applyOrderBy.call(viewModel, query);
	};

	console.log("DispatchDetailsChecklistsTabViewModelExtension loaded successfully");
})();
