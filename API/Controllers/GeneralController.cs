using API.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class GeneralController<TIEntityRepository, TEntity> : ControllerBase
        where TIEntityRepository : IGeneralRepository<TEntity>
    {
        //protected readonly TIEntityRepository _repository;

        //public GeneralController(TIEntityRepository repository)
        //{
        //    _repository = repository;
        //}

        //[HttpGet]
        //public IActionResult GetAll()
        //{
        //    var entity = _repository.GetAll();

        //    if (!entity.Any())
        //    {
        //        return NotFound(new ResponseHandler<ICollection<TEntity>>
        //        {
        //            Code = StatusCodes.Status404NotFound,
        //            Status = HttpStatusCode.NotFound.ToString(),
        //            Message = "Data Tidak Ditemukan"
        //        });
        //    }
        //    return Ok(new ResponseHandler<ICollection<TEntity>>
        //    {
        //        Code = StatusCodes.Status200OK,
        //        Status = HttpStatusCode.OK.ToString(),
        //        Message = "Data Ditemukan",
        //        Data = entity
        //    });
        //}

        //[HttpGet("{guid}")]
        //public IActionResult GetByGuid(Guid guid)
        //{
        //    var entity = _repository.GetByGuid(guid);
        //    if (entity is null)
        //    {
        //        return NotFound(new ResponseHandler<TEntity>
        //        {
        //            Code = StatusCodes.Status404NotFound,
        //            Status = HttpStatusCode.NotFound.ToString(),
        //            Message = "Guid Tidak Ditemukan"
        //        });
        //    }
        //    return Ok(new ResponseHandler<TEntity>
        //    {
        //        Code = StatusCodes.Status200OK,
        //        Status = HttpStatusCode.OK.ToString(),
        //        Message = "Guid Ditemukan",
        //        Data = entity
        //    });
        //}

        //[HttpPost]
        //public IActionResult Create(TEntity entity)
        //{
        //    var isCreated = _repository.Create(entity);
        //    return Ok(isCreated);
        //}

        //[HttpPut]
        //public IActionResult Update(TEntity entity)
        //{
        //    var isUpdated = _repository.Update(entity);
        //    if (!isUpdated)
        //    {
        //        return NotFound(new ResponseHandler<TEntity>
        //        {
        //            Code = StatusCodes.Status404NotFound,
        //            Status = HttpStatusCode.NotFound.ToString(),
        //            Message = "Data Tidak Ditemukan"
        //        });
        //    }
        //    return Ok(new ResponseHandler<TEntity>
        //    {
        //        Code = StatusCodes.Status200OK,
        //        Status = HttpStatusCode.OK.ToString(),
        //        Message = "Data Berhasil Di Update"
        //    });
        //}

        //[HttpDelete("{guid}")]
        //public IActionResult Delete(Guid guid)
        //{
        //    var isDeleted = _repository.Delete(guid);
        //    if (!isDeleted)
        //    {
        //        return NotFound(new ResponseHandler<TEntity>
        //        {
        //            Code = StatusCodes.Status404NotFound,
        //            Status = HttpStatusCode.NotFound.ToString(),
        //            Message = "Data Tidak Ditemukan"
        //        });
        //    }
        //    return Ok(new ResponseHandler<TEntity>
        //    {
        //        Code = StatusCodes.Status200OK,
        //        Status = HttpStatusCode.OK.ToString(),
        //        Message = "Data Berhasil Di Deleted"
        //    });
        //}
    }
}

