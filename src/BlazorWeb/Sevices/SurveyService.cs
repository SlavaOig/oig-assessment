using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using System.Text.Json;
using BlazorWeb.Pages.Survey;
using BlazorWeb.Shared.Models;
using Microsoft.JSInterop;
using oig_assessment.Application.DTOs.Survey;
using oig_assessment.Domain.Entities;
using BlazorBootstrap;
using BlazorWeb.Shared.Models.Survey;

namespace BlazorWeb.Sevices;

public class SurveyService
{
    private readonly HttpClient _httpClient;

    public SurveyService(IHttpClientFactory httpClientFactory) 
    {
        _httpClient = httpClientFactory.CreateClient("AuthorizedClient");
    }


    public async Task<Guid?> CreateSurveyAsync(CreateSurveyDto sutveyToCreate) 
    {
        var response = await _httpClient.PostAsJsonAsync("/api/surveys", sutveyToCreate);

        if (response.IsSuccessStatusCode)
        {
            var createdSurveyId = await response.Content.ReadFromJsonAsync<Guid>();

            return createdSurveyId;
        }
        else
        {
            var error = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Error save survey: {error}");
            return null;
        }
    }




    public async Task<PagedResult<SurveyDto>> GetSurveysAsync(string sortBy, SortDirection sortDirection, int page = 1, int pageSize = 10)
    {
        try
        {
            var response = await _httpClient.GetAsync($"/api/surveys?page={page}&pageSize={pageSize}&sortBy={sortBy}&sortDirection={sortDirection}");

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    Converters = { new SurveyStatusConverter() }
                };

                var pagedResult = await response.Content.ReadFromJsonAsync<PagedResult<SurveyDto>>(options);
                return pagedResult;
            }
            else
            {
                var error = await response.Content.ReadAsStringAsync();
                Console.Error.WriteLine($"Error: {error}");
                return null;
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Exception: {ex.Message}");
            return null;
        }
    }

    public async Task<UpdateSurveyDetailsDto> GetSurveyByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"/api/surveys/{id}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<UpdateSurveyDetailsDto>();
        }

        return null;
    }


    public async Task<bool> UpdateSurveyAsync(UpdateSurveyDetailsDto surveyToUpdate)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/surveys/{surveyToUpdate.Id}", surveyToUpdate);

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> RemoveSurveyAsync(string surveyIdToRemove)
    {
        var response = await _httpClient.DeleteAsync($"api/surveys/{surveyIdToRemove}");

        return response.IsSuccessStatusCode;
    }

    public async Task<bool> ChangeSurveyStatusAsync(Guid surveyId, int newStatus)
    {
        var response = await _httpClient.PutAsJsonAsync($"api/surveys/{surveyId}/statusChange", new { NewStatusValue = newStatus, SurveyId = surveyId });
        return response.IsSuccessStatusCode;
    }

    public async Task<List<PublicQuestionDto>> GetSurveyQuestionsByIdAsync(Guid id)
    {
        var response = await _httpClient.GetAsync($"/api/answers/{id}");

        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<PublicQuestionDto>>();
        }

        return null;
    }

    public async Task SaveAnswers(List<CreateAnswerQuestion> answers)
    {
        await _httpClient.PostAsJsonAsync($"api/answers",new { Answers = answers } );
    }
}
