__author__ = 'Colin Basnett'

import struct
import os
import glob
import sys

class model_t:
    def __init__(self):
        self.name = ""
        self.meshes = list()
        self.aabb = aabb_t()
        self.materials = list()

class mesh_t:
    def __init__(self):
        self.vertices = list()
        self.faces = list()
        self.material_indices = list()
        self.uvs = list()

    def write(self, path):
        out = open(path, "w+")

        out.write("ply\n")
        out.write("format ascii 1.0\n")
        out.write("element vertex " + str(len(self.vertices)) + "\n")
        out.write("property float x\n")
        out.write("property float y\n")
        out.write("property float z\n")
        out.write("element face " + str(len(self.faces)) + "\n")
        out.write("property list uchar int vertex_index\n")
        out.write("end_header\n")

        for j in range(0, len(self.vertices)):
            vertex = self.vertices[j]
            out.write(str(vertex.position.x) + " " + str(vertex.position.y) + " " + str(vertex.position.z) + "\n")

        for j in range(0, len(self.faces)):
            face = self.faces[j]
            out.write("3 " + str(face.indices[2]) + " " + str(face.indices[1]) + " " + str(face.indices[0]) + "\n")

        out.close()

class vertex3_t:
    def __init__(self):
        self.position = vec3_t()
        self.uvs = list()

class color4_t:
    def __init__(self):
        self.r = 0
        self.g = 0
        self.b = 0
        self.a = 0

class vec2_t:
    def __init__(self):
        self.x = 0.0
        self.y = 0.0

class vec3_t:
    def __init__(self):
        self.x = 0.0
        self.y = 0.0
        self.z = 0.0

    def __str__(self):
        return "(" + str(self.x) + ", " + str(self.y) + ", " + str(self.z) + ")"

class aabb_t:
    def __init__(self):
        self.min = vec3_t()
        self.max = vec3_t()

    def __str__(self):
        return "(min:" + str(self.min) + ", max:" + str(self.max) + ")"

class face3_t:
    def __init__(self):
        self.indices = list()

    def __str__(self):
        return "(" + str(self.indices[0]) + ", " + str(self.indices[1]) + ", " + str(self.indices[2]) + ")"

def main(argv=None):
    files = glob.glob("./import/*.dme")

    for file in files:
        f = open(file, "rb")

        model = model_t()

        model.name = os.path.basename(file)
        model.name = os.path.splitext(model.name)[0]

        print model.name

        f.read(4) #dmod
        buffer = f.read(4) #dmod version?
        dmod_version = struct.unpack("i", buffer[0:4])[0]

        buffer = f.read(4)
        model_header_offset = struct.unpack("i", buffer[0:4])[0]

        f.read(4) #dmat
        buffer = f.read(4) #dmat version?
        dmat_version = struct.unpack("i", buffer[0:4])[0]
        buffer = f.read(4)
        materials_length = struct.unpack("i", buffer[0:4])[0]

        buffer = f.read(materials_length)

        model.materials = buffer.split('\0')
        model.materials.pop() #remove last entry (empty)

        print model.materials

        # some block starts here, not sure what it is, appears to be a series of indices into some list with some
        # unknown huge integers strewn here and there.  could be bones or could be a continuation of DMAT information.

        unknown0 = f.read(4) #unknown
        unknown1 = f.read(4) #unknown
        buffer = f.read(4)
        unknown_block_length = struct.unpack("i", buffer[0:4])[0]

        print("unknown_block_length: " + str(unknown_block_length))

        f.seek(model_header_offset)
        buffer = f.read(40) #model header

        model.aabb.min.x = struct.unpack("f", buffer[12:16])[0]
        model.aabb.min.y = struct.unpack("f", buffer[16:20])[0]
        model.aabb.min.z = struct.unpack("f", buffer[20:24])[0]
        model.aabb.max.x = struct.unpack("f", buffer[24:28])[0]
        model.aabb.max.y = struct.unpack("f", buffer[28:32])[0]
        model.aabb.max.z = struct.unpack("f", buffer[32:36])[0]
        mesh_count = struct.unpack("i", buffer[36:40])[0]

        print("box: " + str(model.aabb))
        print("mesh_count: " + str(mesh_count))
        print("materials: " + str(len(model.materials)))

        for i in range(0, mesh_count):
            print "---------"

            mesh = mesh_t()

            index_count = 0
            vertex_count = 0
            bytes_per_vertex = 0

            # mesh header

            if dmod_version == 3: #dmod_version 3
                buffer = f.read(32)

                bytes_per_vertex = struct.unpack("i", buffer[16:20])[0]
                vertex_count = struct.unpack("i", buffer[20:24])[0]
                #mesh.material_index = struct.unpack("I", buffer[24:28])[0] #unsure if correct
                index_count = struct.unpack("i", buffer[28:32])[0]
            elif dmod_version == 4:
                buffer = f.read(36)

                unknown2 = struct.unpack("i", buffer[0:4])[0]               # count or index
                unknown3 = struct.unpack("i", buffer[4:8])[0]               # count or index
                unknown4 = struct.unpack("i", buffer[8:12])[0]              # count or index
                unknown5 = struct.unpack("i", buffer[12:16])[0]             # unknown
                unknown6 = struct.unpack("i", buffer[16:20])[0]             # count or index
                unknown7 = struct.unpack("i", buffer[20:24])[0]             # count or index
                index_count = struct.unpack("i", buffer[24:28])[0]          # index count
                vertex_count = struct.unpack("i", buffer[28:32])[0]         # vertex count
                bytes_per_vertex = struct.unpack("i", buffer[32:36])[0]     # bytes per vertex

            print("index_count: " + str(index_count))
            print("vertex_count: " + str(vertex_count))
            print("bytes_per_vertex: " + str(bytes_per_vertex))

            for j in range(0, vertex_count):
                buffer = f.read(bytes_per_vertex)

                vertex = vertex3_t()
                vertex.position.x = struct.unpack("f", buffer[0:4])[0]
                vertex.position.y = struct.unpack("f", buffer[4:8])[0]
                vertex.position.z = struct.unpack("f", buffer[8:12])[0]

                mesh.vertices.append(vertex)

            # dmod version 4 stores additional (non-position) data vertex data in a separate block immediately after
            # indices block.
            # this *possibly* compressed UV coordinates for each texture channel in the mesh.
            if dmod_version == 4:
                buffer = f.read(4)
                bytes_per_vertex = struct.unpack("i", buffer[0:4])[0]

                print("bytes_per_vertex (2): " + str(bytes_per_vertex))

                print f.tell()

                buffer = f.read(vertex_count * bytes_per_vertex)

                for j in range(0, len(mesh.vertices)):
                    # uv-coordinates components (x,y) are compressed to 2 16-byte integers
                    # representing a ratio of
                    mesh.vertices[j].uvs.append(float(struct.unpack("H", buffer[12:14])[0]) / 65535.0)
                    mesh.vertices[j].uvs.append(float(struct.unpack("H", buffer[14:16])[0]) / 65535.0)

            for j in range(0, index_count / 3):
                buffer = f.read(6)

                face = face3_t()
                face.indices.append(struct.unpack("H", buffer[0:2])[0])
                face.indices.append(struct.unpack("H", buffer[2:4])[0])
                face.indices.append(struct.unpack("H", buffer[4:6])[0])

                mesh.faces.append(face)

            model.meshes.append(mesh)

            mesh.write("./export/" + model.name + "_" + str(i) + ".ply")

        f.close()

if __name__ == "__main__":
    sys.exit(main())