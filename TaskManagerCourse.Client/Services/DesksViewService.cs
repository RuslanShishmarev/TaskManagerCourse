using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using TaskManagerCourse.Client.Models;
using TaskManagerCourse.Client.Views.AddWindows;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Client.Services
{
    public class DesksViewService
    {
        private AuthToken _token;
        private DesksRequestService _desksRequestService;
        private CommonViewService _viewService;
        public DesksViewService(AuthToken token, DesksRequestService desksRequestService)
        {
            _token = token;
            _desksRequestService = desksRequestService;
            _viewService = new CommonViewService();
        }

        public ModelClient<DeskModel> GetDeskClientById(object deskId)
        {
            try
            {
                int id = (int)deskId;
                DeskModel desk = _desksRequestService.GetDeskById(_token, id);
                return new ModelClient<DeskModel>(desk);
            }
            catch (FormatException)
            {
                return new ModelClient<DeskModel>(null);
            }
        }

        public List<ModelClient<DeskModel>> GetDesks(int projectId)
        {
            var result = new List<ModelClient<DeskModel>>();
            var desks = _desksRequestService.GetDesksByProject(_token, projectId);
            if (desks != null)
            {
                result = desks.Select(d => new ModelClient<DeskModel>(d)).ToList();
            }
            return result;
        }
        public List<ModelClient<DeskModel>> GetAllDesks()
        {
            var result = new List<ModelClient<DeskModel>>();
            var desks = _desksRequestService.GetAllDesks(_token);
            if (desks != null)
            {
                result = desks.Select(d => new ModelClient<DeskModel>(d)).ToList();
            }
            return result;
        }

        public void OpenViewDeskInfo(object deskId, BindableBase context)
        {
            var wnd = new CreateOrUpdateDeskWindow();
            _viewService.OpenWindow(wnd, context);
        }

        public void UpdateDesk(DeskModel desk)
        {

            var resultAction = _desksRequestService.UpdateDesk(_token, desk);
            _viewService.ShowActionResult(resultAction, "New project is updated");
        }

        public void DeleteDesk(int deskId)
        {
            var resultAction = _desksRequestService.DeleteDesk(_token, deskId);
            _viewService.ShowActionResult(resultAction, "New project is deleted");
        }

        public ModelClient<DeskModel> SelectPhotoForDesk(ModelClient<DeskModel> selectedDesk)
        {
            if(selectedDesk?.Model != null)
            {
                _viewService.SetPhotoForObject(selectedDesk.Model);
                selectedDesk = new ModelClient<DeskModel>(selectedDesk.Model);
            }
            return selectedDesk;
        }
    }
}
