﻿using System.Net;
using API.DTOS.Bookings;
using API.Services;
using API.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingController : ControllerBase
    {
        private readonly BookingService _service;

        public BookingController(BookingService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var entities = _service.GetBooking();

            if (entities == null)
            {
                return NotFound(new ResponseHandler<GetBookingsDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<GetBookingsDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data found",
                Data = entities
            });
        }

        [HttpGet("{guid}")]
        public IActionResult GetByGuid(Guid guid)
        {
            var booking = _service.GetBooking(guid);
            if (booking is null)
            {
                return NotFound(new ResponseHandler<GetBookingsDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            return Ok(new ResponseHandler<GetBookingsDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data found",
                Data = booking
            });
        }

        [HttpPost]
        public IActionResult Create(NewBookingsDto newBookingDto)
        {
            var createBooking = _service.CreateBooking(newBookingDto);
            if (createBooking is null)
            {
                return BadRequest(new ResponseHandler<GetBookingsDto>
                {
                    Code = StatusCodes.Status400BadRequest,
                    Status = HttpStatusCode.BadRequest.ToString(),
                    Message = "Data not created"
                });
            }

            return Ok(new ResponseHandler<GetBookingsDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully created",
                Data = createBooking
            });
        }

        [HttpPut]
        public IActionResult Update(UpdateBookingsDto updateBookingDto)
        {
            var update = _service.UpdateBooking(updateBookingDto);
            if (update is -1)
            {
                return NotFound(new ResponseHandler<UpdateBookingsDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id not found"
                });
            }
            if (update is 0)
            {
                return BadRequest(new ResponseHandler<UpdateBookingsDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check your data"
                });
            }
            return Ok(new ResponseHandler<UpdateBookingsDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully updated"
            });
        }

        [HttpDelete]
        public IActionResult Delete(Guid guid)
        {
            var delete = _service.DeleteBooking(guid);

            if (delete is -1)
            {
                return NotFound(new ResponseHandler<GetBookingsDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Id not found"
                });
            }
            if (delete is 0)
            {
                return BadRequest(new ResponseHandler<GetBookingsDto>
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Check connection to database"
                });
            }

            return Ok(new ResponseHandler<GetBookingsDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Successfully deleted"
            });
        }

        [Route("bookingsLength")]
        [HttpGet]
        public IActionResult CalculateBookingLength()
        {
            var entities = _service.GetBookingLength();

            if (entities == null)
            {
                return NotFound(new ResponseHandler<BookingLengthDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<BookingLengthDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data found",
                Data = entities
            });
        }

        [HttpGet("Booking Details")]
        public IActionResult GetBookingDetails()
        {
            var bookingDetails = _service.GetBookingDetails();
            if (bookingDetails == null)
            {
                return NotFound(new ResponseHandler<GetBookingsDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            return Ok(new ResponseHandler<IEnumerable<BookingDetailsDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = bookingDetails
            });
        }

        [HttpGet("BookingDetailsByGuid")]
        public IActionResult GetBookingDetails(Guid guid)
        {
            var bookingDetails = _service.GetBookingDetailByGuid(guid);

            if (bookingDetails == null)
            {
                return NotFound(new ResponseHandler<GetBookingsDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"
                });
            }

            return Ok(new ResponseHandler<BookingDetailsDto>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = bookingDetails
            });
        }

        [HttpGet("RoomUsedToday")]
        public IActionResult GetUsedRoomToday()
        {
            var bookingDetails = _service.GetRoomUsedToday();

            if (bookingDetails == null)
            {
                return NotFound(new ResponseHandler<BookingDetailsDto>
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data not found"

                });
            }

            return Ok(new ResponseHandler<IEnumerable<BookingDetailsDto>>
            {
                Code = StatusCodes.Status200OK,
                Status = HttpStatusCode.OK.ToString(),
                Message = "Data Found",
                Data = bookingDetails
            });
        }
    }
}
