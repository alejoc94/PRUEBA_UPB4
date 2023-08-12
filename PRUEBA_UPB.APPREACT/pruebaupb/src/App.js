import React, {useStatem, useEffect} from 'react';
import { useState } from 'react';
import './App.css';
import 'bootstrap/dist/css/bootstrap.min.css';
import axios from 'axios';
import {Modal, ModalBody, ModalFooter, ModalHeader} from 'reactstrap';


function App() {
  const baseUrl = "https://localhost:7146";
  const baseUrlCons = baseUrl + "/ObtenerUsuarios";
  const baseUrlIns = baseUrl + "/CrearUsuario/";
  const baseUrlAct = baseUrl + "/ActualizarUsuario/";
  const [data, setData] = useState([]);
  const [modalInsertar, setModalInsertar]=useState(false);
  const [modalEditar, setModalEditar]=useState(false);
  const [modalEliminar, setModalEliminar]=useState(false);
  const [usuarioSeleccionado, setUsuarioSeleccionado]=useState({
    id: 0,
    nombres: '',
    apellidos: '',
    email: '',
    username: '',
    pwd: ''
  })

  const handleChange=e=>{
    const {name, value}=e.target;
    setUsuarioSeleccionado({
      ...usuarioSeleccionado,
      [name]: value
    });
    console.log(usuarioSeleccionado);
  }

  const abrirCerrarModalInsertar=()=>{
    setModalInsertar(!modalInsertar);
  }

  const abrirCerrarModalEditar=()=>{
    setModalEditar(!modalEditar);
  }

  const abrirCerrarModalEliminar=()=>{
    setModalEliminar(!modalEliminar);
  }

  const peticionGet=async()=>
  {
    await axios.get(baseUrlCons)
    .then(Response=>{
      setData(Response.data);
    }).catch(error=>{
      console.log(error);
    })
  }  
  
  const seleccionarUsuario=(ObtenerUsuarios, caso)=>{
    setUsuarioSeleccionado(ObtenerUsuarios);
    (caso === "Editar") ? abrirCerrarModalEditar() : abrirCerrarModalEliminar();
  }
    
  const peticionPost=async()=>{
    await axios.post(baseUrlIns, usuarioSeleccionado)
    .then(Response=>{
      setData(data.concat(Response.data));
      abrirCerrarModalInsertar();
    }).catch(error=>{
      console.log(error);
    })
  }  

  const peticionPut=async()=>{
    await axios.put(baseUrlAct, usuarioSeleccionado)
    .then(response=>{
      var respuesta = response.data;
      var dataAuxiliar = data;
      dataAuxiliar.map(usuario=>{
        if(usuario.id===usuarioSeleccionado.id)
        {
          usuario.nombres = usuarioSeleccionado.nombres;
          usuario.apellidos = usuarioSeleccionado.apellidos;
          usuario.email = usuarioSeleccionado.email;
          usuario.username = usuarioSeleccionado.username;
          usuario.pwd = usuarioSeleccionado.pwd;
        }
      })
      abrirCerrarModalEditar();
    }).catch(error=>{
      console.log(error);
    })
  } 

  const peticionDelete=async()=>{
    await axios.delete(baseUrl+"/"+usuarioSeleccionado.id)
    .then(Response=>{
      setData(data.filter(usuario=>usuario.id!==usuarioSeleccionado.id));
      abrirCerrarModalEliminar();
    }).catch(error=>{
      console.log(error);
    })
  } 

  useEffect(()=>{
    peticionGet();
  },[]  )  

  return (
    <div className="App">
    <br /> <br />
    <button className="btn btn-success" onClick={()=>abrirCerrarModalInsertar()}>Insertar Nuevo Usuario</button>
    <table className="table table-bordered">
      <thead>
        <tr>
          <th>Id</th>
          <th>Nombres</th>
          <th>Apellidos</th>
          <th>Email</th>
          <th>Username</th>
          <th>Clave</th>
          <th> Acciones</th>
        </tr>
      </thead>
      <tbody>
        {data.map(ObtenerUsuarios=>(
          <tr>
            <td>{ObtenerUsuarios.id}</td>
            <td>{ObtenerUsuarios.nombres}</td>
            <td>{ObtenerUsuarios.apellidos}</td>
            <td>{ObtenerUsuarios.email}</td>
            <td>{ObtenerUsuarios.username}</td>
            <td type="password">{ObtenerUsuarios.pwd}</td>
            <td>
              <button className="btn btn-primary" onClick={()=>seleccionarUsuario(ObtenerUsuarios, "Editar")}>Editar</button>
              <button className="btn btn-danger"  onClick={()=>seleccionarUsuario(ObtenerUsuarios, "Eliminar")}>Borrar</button>
            </td>
          </tr>
        ))}
      </tbody>
    </table>

    <Modal isOpen={modalInsertar}>
    <ModalHeader>Insertar Usuarios</ModalHeader>  
    <ModalBody>
      <div className="form-group">
          <label>Id:</label>
          <br />
          <input type="number" className="form-control"  name="id" onChange={handleChange}/>

          <label>Nombres:</label>
          <br />
          <input type="text" className="form-control" name="nombres" onChange={handleChange}/>

          <label>Apellidos:</label>
          <br />
          <input type="text" className="form-control" name="apellidos" onChange={handleChange}/>

          <label>Email:</label>
          <br />
          <input type="text" className="form-control" name="email" onChange={handleChange}/>

          <label>Username:</label>
          <br />
          <input type="text" className="form-control" name="username" onChange={handleChange}/>

          <label>Clave:</label>
          <br />
          <input type="password" className="form-control" name="pwd" onChange={handleChange}/>      
        
      </div>
      <ModalFooter> 
         <button className="btn btn-primary" onClick={()=>peticionPost()}>Insertar</button>
         <button className="btn btn-danger" onClick={()=>abrirCerrarModalInsertar()}>Cancelar</button>
      </ModalFooter>
    </ModalBody>
    </Modal>

    <Modal isOpen={modalEditar}>
    <ModalHeader>Modificar Usuario</ModalHeader>  
    <ModalBody>
      <div className="form-group">
          <label>Id:</label>
          <br />
          <input type="number" className="form-control"  name="id" onChange={handleChange} readOnly value={usuarioSeleccionado && usuarioSeleccionado.id} />

          <label>Nombres:</label>
          <br />
          <input type="text" className="form-control" name="nombres" onChange={handleChange} value={usuarioSeleccionado && usuarioSeleccionado.nombres} />

          <label>Apellidos:</label>
          <br />
          <input type="text" className="form-control" name="apellidos" onChange={handleChange} value={usuarioSeleccionado && usuarioSeleccionado.apellidos} />

          <label>Email:</label>
          <br />
          <input type="text" className="form-control" name="email" onChange={handleChange} value={usuarioSeleccionado && usuarioSeleccionado.email} />

          <label>Username:</label>
          <br />
          <input type="text" className="form-control" name="username" onChange={handleChange} value={usuarioSeleccionado && usuarioSeleccionado.username} />

          <label>Clave:</label>
          <br />
          <input type="password" className="form-control" name="pwd" onChange={handleChange} value={usuarioSeleccionado && usuarioSeleccionado.pwd} />      
         
      </div>
      <ModalFooter> 
         <button className="btn btn-primary" onClick={()=>peticionPut()}>Editar</button>
         <button className="btn btn-danger" onClick={()=>abrirCerrarModalEditar()}>Cancelar</button>
      </ModalFooter>
    </ModalBody>
    </Modal>

    <Modal isOpen={modalEliminar}>    
    <ModalBody>
    ¿Está seguro de que desea borrar el usuario {usuarioSeleccionado && usuarioSeleccionado.nombres}?
    </ModalBody>
    <ModalFooter>
      <button className="btn btn-danger" onClick={()=>peticionDelete()}>
        Sí
      </button>
      <button className="btn btn-secondary" onClick={()=>abrirCerrarModalEliminar()}>
        No
      </button>
    </ModalFooter>
    
    </Modal>

    </div>
  );
}

export default App;
