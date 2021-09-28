import http from "../http-common";
import ITutorialData from "../types/Tutorial";

const getAll = async () => {
  const response = await http.get("/tutorial");
  return response.data;
};

const get = async (id: any) => {
  const response = await http.get(`/tutorial/${id}`);
  return response.data;
};

const create = async (data: ITutorialData) => {
  const response = await http.post("/tutorial", data);
  return response.data;
};

const update = async (data: ITutorialData) => {
  const response = await http.put(`/tutorial`, data);
  return response.data;
};

const remove = async (id: any) => {
  const response = await http.delete(`/tutorial/${id}`);
  return response.data;
};

const removeAll = async () => {
  const response = await http.delete(`/tutorial/all`);
  return response.data;
};

const findByTitle = async (title: string) => {
  const response = await http.get(`/tutorial/find/${title}`);
  return response.data;
};

const TutorialService = {
  getAll,
  get,
  create,
  update,
  remove,
  removeAll,
  findByTitle,
};

export default TutorialService;