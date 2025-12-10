(function () {
	// Check if base exists
	if (!window.Crm || !window.Crm.Service || !window.Crm.Service.ViewModels || !window.Crm.Service.ViewModels.ServiceOrderDetailsChecklistsTabViewModel) {
		console.error("ServiceOrderDetailsChecklistsTabViewModel base not found - extension cannot load");
		return;
	}

	var basePrototype = window.Crm.Service.ViewModels.ServiceOrderDetailsChecklistsTabViewModel.prototype;

	// Override applyOrderBy to prioritize checklists for the currently selected job
	basePrototype.applyOrderBy = function (query) {
		var viewModel = this;
		var currentServiceOrderTimeId = null;

		// Get the current ServiceOrderTime ID from the parent's dispatch or service order context
		if (viewModel.parentViewModel && viewModel.parentViewModel.dispatch && viewModel.parentViewModel.dispatch()) {
			var dispatch = viewModel.parentViewModel.dispatch();
			if (dispatch.CurrentServiceOrderTimeId) {
				currentServiceOrderTimeId = dispatch.CurrentServiceOrderTimeId();
			}
		}

		// Apply ordering to prioritize current job's checklists at the top
		if (currentServiceOrderTimeId) {
			query = query.orderByDescending("orderByCurrentServiceOrderTime", { currentServiceOrderTimeId: currentServiceOrderTimeId });
		}

		// Call the parent's applyOrderBy if it exists
		if (window.Crm.Service.ViewModels.DispatchDetailsChecklistsTabViewModel.prototype.applyOrderBy) {
			return window.Crm.Service.ViewModels.DispatchDetailsChecklistsTabViewModel.prototype.applyOrderBy.call(viewModel, query);
		}

		return window.Main.ViewModels.GenericListViewModel.prototype.applyOrderBy.call(viewModel, query);
	};

	console.log("ServiceOrderDetailsChecklistsTabViewModelExtension loaded successfully");
})();
